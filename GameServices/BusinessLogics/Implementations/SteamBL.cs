﻿using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SteamBL(IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IIgnoredSteamGameRepository ignoredSteamGameRepository,
        IPlatformRepository platformRepository,
        ISteamApiGateway steamApiGateway) : ISteamBL
    {
        public async Task<Guid> AddSteamGame(SteamGameDto gameSteamDto)
        {
            var steamPlatformId = platformRepository.FindSelect(p => p.PlatformEnum == PlatformEnumEntity.Steam, f => f.Select(p => p.Id));
            var achievementsResult = steamApiGateway.GetAchievementByAppId(gameSteamDto.SteamId);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(gameSteamDto.SteamId);
            await Task.WhenAll(achievementsResult, percentagesResult, steamPlatformId);

            var game = await gameRepository.InsertAndSave(gameSteamDto.ToEntity(achievementsResult.Result, percentagesResult.Result, steamPlatformId.Result));

            return game.Id;
        }

        public async Task<List<SteamGameDto>> GetMissingSteamGames()
        {
            var steamIds = await gameDetailRepository.GetSelect(f => f.Select(g => g.SteamId), g => g.SteamId != null);
            var ignoredSteamIds = await ignoredSteamGameRepository.GetSelect(i => i.Select(i => (int?)i.SteamId));
            var steamGames = await steamApiGateway.GetSteamGames();

            return steamGames?.Where(sg => !steamIds.Union(ignoredSteamIds).Contains(sg.appid)).OrderBy(sg => sg.name).Select(sg => sg.ToDto()).ToList() ?? [];
        }

        public async Task<int> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var ignoredSteamGame = gameSteamDto.ToEntity();
                await ignoredSteamGameRepository.InsertAndSave(ignoredSteamGame);
            }

            return gameSteamDto.SteamId;
        }

        public async Task ReloadSteamGame(Guid gameDetailId)
        {
            var game = await gameDetailRepository.Find(g => g.Id == gameDetailId, f => f.Include(g => g.Achievements), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameDetailId}] was not found.");

            if (!game.SteamId.HasValue)
                throw new ValidationException($"The game with id [{gameDetailId}] has no steamId and can't be reloaded.");

            var achievementsResult = steamApiGateway.GetAchievementByAppId(game.SteamId.Value);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(game.SteamId.Value);
            await Task.WhenAll(achievementsResult, percentagesResult);

            game.Achievements?.ForEach(a =>
            {
                a.Achieved = achievementsResult.Result.FirstOrDefault(ac => ac.apiname == a.SteamName)?.achieved != 0;
                a.Percentage = percentagesResult.Result.FirstOrDefault(p => p.name == a.SteamName)?.percent;
            });

            await gameRepository.SaveChanges();
        }
    }
}
