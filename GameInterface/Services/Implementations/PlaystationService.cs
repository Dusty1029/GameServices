﻿using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlaystationService(IGenericService genericService) : IPlaystationService
    {
        private readonly string beginPath = "playstation";
        public Task<ApiResult<Guid>> AddPlaystationGame(CreatePlaystationGameDto gamePlaystationDto) => genericService.PostResult<Guid>(gamePlaystationDto, beginPath);

        public Task<ApiResult<List<PlaystationGameDto>>> GetMissingPlaystationGames() => genericService.GetResult<List<PlaystationGameDto>>(beginPath);

        public Task<ApiResult> IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored) => genericService.PostResult(gamePlaystationDto, $"{beginPath}/ignore/{isIgnored}");

        public Task<ApiResult> RefreshToken(string npsso) => genericService.PutResult(path: $"{beginPath}/token/{npsso}");

        public Task<ApiResult> ReloadPlaystationGame(Guid gameDetailId) => genericService.PutResult(path: $"{beginPath}/game/{gameDetailId}/reload");
    }
}
