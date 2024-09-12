﻿using CommonV2.Models;
using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetGameById(Guid id);
        Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto);
    }
}
