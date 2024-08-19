using CommonV2.Infrastructure.Services.Interfaces;
using GameServices.API.Dtos.SteamGateway;
using GameServices.API.Gateways.Interfaces;
using GameServices.API.Models.Options;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Web;

namespace GameServices.API.Gateways.Implementations
{
    public class SteamApiGateway : ISteamApiGateway
    {
        private readonly CancellationToken _cancellationToken;
        private readonly HttpClient _httpClient;
        private readonly SteamOptions _steamOptions;
        public SteamApiGateway(IOptions<SteamOptions> steamOptions,
            ICancellationTokenService cancellationTokenService,
            HttpClient httpClient) 
        {
            _steamOptions = steamOptions.Value;
            _httpClient = httpClient;
            _cancellationToken = cancellationTokenService.CancellationToken;
        }

        public async Task<List<GameSteamDto>?> GetSteamGames()
        {
            var builder = new UriBuilder(_steamOptions.Url + "/IPlayerService/GetOwnedGames/v0001/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["steamid"] = _steamOptions.SteamId;
            query["include_appinfo"] = true.ToString();
            builder.Query = query.ToString();
            
            var response = await _httpClient.GetFromJsonAsync<ResponseGetGamesSteamDto>(builder.ToString(), _cancellationToken);
            return response?.response?.games;
        }

        public async Task<List<AchievementSteamDto>> GetAchievementByAppId(int appId)
        {
            var builder = new UriBuilder(_steamOptions.Url + "/ISteamUserStats/GetPlayerAchievements/v0001/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["appid"] = appId.ToString();
            query["steamid"] = _steamOptions.SteamId;
            query["l"] = "french";
            builder.Query = query.ToString();

            var response = await _httpClient.GetFromJsonAsync<ResponseGetAchievementsSteamDto>(builder.ToString(), _cancellationToken);
            return response!.playerstats.achievements;
        }

        public async Task<List<AchievementPercentageSteamDto>> GetAchievementPercentageByAppId(int appId)
        {
            var builder = new UriBuilder(_steamOptions.Url + "/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["gameId"] = appId.ToString();
            builder.Query = query.ToString();

            var response = await _httpClient.GetFromJsonAsync<ResponseGetPercentageSteamDto>(builder.ToString(), _cancellationToken);
            return response!.achievementpercentages.achievements;
        }

        private void AddBasicQueryParams(NameValueCollection queryParams)
        {
            queryParams["key"] = _steamOptions.Key;
            queryParams["format"] = "json";
        }
    }
}
