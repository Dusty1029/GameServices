﻿using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.Options;
using GameService.API.Models.XboxGateway;
using Microsoft.Extensions.Options;

namespace GameService.API.Gateways.Implementations
{
    public class XboxApiGateway(ICancellationTokenService cancellationTokenService,
        HttpClient httpClient,
        IOptions<XboxOptions> xboxOptions) : IXboxApiGateway
    {
        private readonly CancellationToken _cancellationToken = cancellationTokenService.CancellationToken;
        private readonly XboxOptions _xboxOptions = xboxOptions.Value;

        public async Task<List<XboxAchievement>> GetXboxAchievementsByGame(string titleId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseGetXboxAchievementsByGame>($"player/{_xboxOptions.UserId}/title/{titleId}", _cancellationToken);
            return response!.achievements;
        }

        public async Task<List<XboxAchievement>> GetXboxAchievementsEarnedByGame(string titleId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseGetXboxAchievementsByGame>($"x360/{_xboxOptions.UserId}/title/{titleId}", _cancellationToken);
            return response!.achievements;
        }

        public async Task<List<XboxGame>> GetXboxGames()
        {
            var response = await httpClient.GetFromJsonAsync<ResponseGetXboxGames>($"player/{_xboxOptions.UserId}", _cancellationToken);
            return response!.titles;
        }
    }
}
