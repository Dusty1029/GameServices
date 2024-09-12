using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Models.SteamGateway;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.Options;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Web;

namespace GameService.API.Gateways.Implementations
{
    public class SteamApiGateway(IOptions<SteamOptions> steamOptions,
        ICancellationTokenService cancellationTokenService,
        HttpClient httpClient) : ISteamApiGateway
    {
        private readonly CancellationToken _cancellationToken = cancellationTokenService.CancellationToken;
        private readonly SteamOptions _steamOptions = steamOptions.Value;

        public async Task<List<GameSteam>?> GetSteamGames()
        {
            var builder = new UriBuilder(_steamOptions.Url + "/IPlayerService/GetOwnedGames/v0001/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["steamid"] = _steamOptions.SteamId;
            query["include_appinfo"] = true.ToString();
            builder.Query = query.ToString();
            
            var response = await httpClient.GetFromJsonAsync<ResponseGetGamesSteam>(builder.ToString(), _cancellationToken);
            return response?.response?.games;
        }

        public async Task<List<AchievementSteam>> GetAchievementByAppId(int appId)
        {
            try
            {
                var builder = new UriBuilder(_steamOptions.Url + "/ISteamUserStats/GetPlayerAchievements/v0001/");
                var query = HttpUtility.ParseQueryString(builder.Query);
                AddBasicQueryParams(query);
                query["appid"] = appId.ToString();
                query["steamid"] = _steamOptions.SteamId;
                query["l"] = "french";
                builder.Query = query.ToString();

                var response = await httpClient.GetFromJsonAsync<ResponseGetAchievementsSteam>(builder.ToString(), _cancellationToken);
                return response!.playerstats.achievements;
            }
            catch (HttpRequestException ex) 
            { 
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest) return [];

                throw;
            }
        }

        public async Task<List<AchievementPercentageSteam>> GetAchievementPercentageByAppId(int appId)
        {
            try
            {
                var builder = new UriBuilder(_steamOptions.Url + "/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/");
                var query = HttpUtility.ParseQueryString(builder.Query);
                AddBasicQueryParams(query);
                query["gameId"] = appId.ToString();
                builder.Query = query.ToString();

                var response = await httpClient.GetFromJsonAsync<ResponseGetPercentageSteam>(builder.ToString(), _cancellationToken);
                return response!.achievementpercentages.achievements;
            }
            catch (HttpRequestException ex) 
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden) return [];

                throw;
            }    
        }

        private void AddBasicQueryParams(NameValueCollection queryParams)
        {
            queryParams["key"] = _steamOptions.Key;
            queryParams["format"] = "json";
        }
    }
}
