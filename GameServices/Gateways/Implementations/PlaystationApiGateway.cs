using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using UnauthorizedAccessException = CommonV2.Models.Exceptions.UnauthorizedAccessException;

namespace GameService.API.Gateways.Implementations
{
    public class PlaystationApiGateway(ICancellationTokenService cancellationTokenService,
        HttpClient httpClient,
        IOptions<PlaystationOption> playstationOption) : IPlaystationApiGateway
    {
        private readonly CancellationToken _cancellationToken = cancellationTokenService.CancellationToken;
        private readonly PlaystationOption _playstationOption = playstationOption.Value;

        public async Task<string?> GetAuthenticationToken(string npsso)
        {
            var builder = new UriBuilder($"{_playstationOption.TokenUrl}authorize");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_type"] = "offline";
            query["client_id"] = "09515159-7237-4370-9b40-3806e67c0891";
            query["response_type"] = "code";
            query["scope"] = "psn:mobile.v2.core psn:clientapp";
            query["redirect_uri"] = "com.scee.psxandroid.scecompcall://redirect";
            builder.Query = query.ToString();

            try
            {

                httpClient.DefaultRequestHeaders.Add("Cookie", $"npsso={npsso}");
                var response = await httpClient.GetAsync(builder.ToString());
                var location = response.Headers.Location;
                if (location != null && location.Query.Contains("?code=v3"))
                {
                    var responseQuery = HttpUtility.ParseQueryString(location.Query);
                    var code = responseQuery?["code"];

                    return code is null ? null : await GetToken(code);
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<GamePlaystation>?> GetPlaystationGames(string token)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{_playstationOption.ServiceUrl}users/me/trophyTitles");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["limit"] = "800";
                builder.Query = query.ToString();

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");

                var response = await httpClient.GetFromJsonAsync<ResponseGetGamesPlaystation>(builder.ToString(), _cancellationToken);
                return response?.trophyTitles;

            });

        public Task<List<Trophy>> GetTrophiesByGame(string token, string gameId, PlatformEnumEntity platformEnum)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{_playstationOption.ServiceUrl}npCommunicationIds/{gameId}/trophyGroups/all/trophies");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["npServiceName"] = platformEnum == PlatformEnumEntity.PS5 ? "trophy2" : "trophy";
                builder.Query = query.ToString();

                if (!httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "fr-fr");

                var response = await httpClient.GetFromJsonAsync<ResponseGetTrophiesByGame>(builder.ToString(), _cancellationToken);
                return response!.trophies;
            });

        public Task<List<TrophyEarned>> GetTrophyEarnedsByGame(string token, string gameId, PlatformEnumEntity platformEnum)
            => ApiExceptionHandler(async () =>
            {
                var builder = new UriBuilder($"{_playstationOption.ServiceUrl}users/me/npCommunicationIds/{gameId}/trophyGroups/all/trophies");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["npServiceName"] = platformEnum == PlatformEnumEntity.PS5 ? "trophy2" : "trophy";
                builder.Query = query.ToString();

                if (!httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");

                var response = await httpClient.GetFromJsonAsync<ResponseGetTrophiesEarnedByGame>(builder.ToString(), _cancellationToken);
                return response!.trophies;
            });

        private async Task<string?> GetToken(string code)
        {
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "com.scee.psxandroid.scecompcall://redirect"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("token_format", "jwt")
            });

            string url = $"{_playstationOption.TokenUrl}token";
            try
            {

                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                var response = await httpClient.PostAsync(url, body);
                var content = await response.Content.ReadAsStringAsync();

                // Suppose the content is in JSON format
                dynamic? json = JsonConvert.DeserializeObject(content);
                if (json is not null)
                {
                    return json.access_token;
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async static Task<TResult> ApiExceptionHandler<TResult>(Func<Task<TResult>> action)
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
