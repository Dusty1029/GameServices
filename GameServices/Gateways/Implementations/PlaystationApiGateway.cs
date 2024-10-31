using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;
using GameService.API.Gateways.Interfaces;
using System.Net;
using System.Web;
using UnauthorizedAccessException = CommonV2.Models.Exceptions.UnauthorizedAccessException;

namespace GameService.API.Gateways.Implementations
{
    public class PlaystationApiGateway(ICancellationTokenService cancellationTokenService,
        HttpClient httpClient,
        IMemoryCacheService cacheService) : IPlaystationApiGateway
    {
        private readonly CancellationToken _cancellationToken = cancellationTokenService.CancellationToken;
        private readonly string PlaystationGamesCacheKey = "playstationGamesCacheKey";

        public Task<List<GamePlaystation>> GetPlaystationGames(string token, bool forceReload) =>
            cacheService.GetOrAddAsync(PlaystationGamesCacheKey,() => GetPlaystationGamesApi(token), forceReload);

        private Task<List<GamePlaystation>> GetPlaystationGamesApi(string token)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{httpClient.BaseAddress}users/me/trophyTitles");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["limit"] = "800";
                builder.Query = query.ToString();

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");

                var response = await httpClient.GetFromJsonAsync<ResponseGetGamesPlaystation>(builder.ToString(), _cancellationToken);
                return response?.trophyTitles ?? [];

            });

        public Task<List<Trophy>> GetTrophiesByGame(string token, string gameId, PlatformEnumEntity platformEnum)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{httpClient.BaseAddress}npCommunicationIds/{gameId}/trophyGroups/all/trophies");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["npServiceName"] = platformEnum == PlatformEnumEntity.PS5 ? "trophy2" : "trophy";
                builder.Query = query.ToString();

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "fr-FR");

                var response = await httpClient.GetFromJsonAsync<ResponseGetTrophiesByGame>(builder.ToString(), _cancellationToken);
                return response?.trophies ?? [];
            });

        public Task<List<TrophyEarned>> GetTrophyEarnedsByGame(string token, string gameId, PlatformEnumEntity platformEnum)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{httpClient.BaseAddress}users/me/npCommunicationIds/{gameId}/trophyGroups/all/trophies");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["npServiceName"] = platformEnum == PlatformEnumEntity.PS5 ? "trophy2" : "trophy";
                builder.Query = query.ToString();

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");

                var response = await httpClient.GetFromJsonAsync<ResponseGetTrophiesEarnedByGame>(builder.ToString(), _cancellationToken);
                return response?.trophies ?? [];
            });

        private static async Task<TResult> ApiExceptionHandler<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                return await action();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("The token is expired.");

                throw;
            }
        }
    }
}
