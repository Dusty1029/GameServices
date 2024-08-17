using CommonV2.Infrastructure.Services.Interfaces;
using GameServices.API.Dtos.SteamGateway;
using GameServices.API.Gateways.Interfaces;
using GameServices.API.Models;
using System.Collections.Specialized;
using System.Web;

namespace GameServices.API.Gateways.Implementations
{
    public class SteamApiGateway : ISteamApiGateway
    {
        private readonly ICancellationTokenService _cancellationTokenService;
        private readonly HttpClient _client = new();
        private readonly SteamOptions _steamOptions;
        public SteamApiGateway(IConfiguration configuration, ICancellationTokenService cancellationTokenService) 
        {
            _steamOptions = configuration.GetSection("Steam").Get<SteamOptions>();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _cancellationTokenService = cancellationTokenService;
        }

        public async Task<List<GameSteamDto>?> GetSteamGames()
        {
            var builder = new UriBuilder(_steamOptions.Url + "/IPlayerService/GetOwnedGames/v0001/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["steamid"] = _steamOptions.SteamId;
            query["include_appinfo"] = true.ToString();
            builder.Query = query.ToString();

            var response = await _client.GetFromJsonAsync<ResponseGetGameSteamDto>(builder.ToString(), _cancellationTokenService.CancellationToken);
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

            var response = await _client.GetFromJsonAsync<ResponseGetAchievementSteamDto>(builder.ToString(), _cancellationTokenService.CancellationToken);
            return response!.playerstats.achievements;
        }

        public async Task<List<AchievementPercentageSteamDto>> GetAchievementPercentageByAppId(int appId)
        {
            var builder = new UriBuilder(_steamOptions.Url + "/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/");
            var query = HttpUtility.ParseQueryString(builder.Query);
            AddBasicQueryParams(query);
            query["gameId"] = appId.ToString();
            builder.Query = query.ToString();

            var response = await _client.GetFromJsonAsync<ResponseGetPercentageSteamDto>(builder.ToString(), _cancellationTokenService.CancellationToken);
            return response!.achievementpercentages.achievements;
        }

        private void AddBasicQueryParams(NameValueCollection queryParams)
        {
            queryParams["key"] = _steamOptions.Key;
            queryParams["format"] = "json";
        }
    }
}
