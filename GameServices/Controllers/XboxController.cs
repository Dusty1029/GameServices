﻿using CommonV2.Helpers.Controller;
using Game.Dto.Xbox;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class XboxController(IControllerExecutor controllerExecutor,
        IXboxBL xboxBL) : ControllerBase
    {
        [HttpGet]
        [Route("force/{forceReload}")]
        public Task<IActionResult> GetMissingXboxGames([FromRoute] bool forceReload)
            => controllerExecutor.ExecuteAsync(this, () => xboxBL.GetMissingXboxGames(forceReload));

        [HttpPost]
        public Task<IActionResult> AddXboxGame([FromBody] CreateXboxGameDto xboxGameDto)
            => controllerExecutor.ExecuteAsync(this, () => xboxBL.AddXboxGame(xboxGameDto));

        [HttpPost]
        [Route("ignore/{isIgnored}")]
        public Task<IActionResult> IgnoreXboxGame([FromBody] XboxGameDto xboxGameDto, [FromRoute] bool isIgnored)
            => controllerExecutor.ExecuteAsync(this, () => xboxBL.IgnoreXboxGame(xboxGameDto, isIgnored));

        [HttpPut]
        [Route("game/{gameId}/reload")]
        public Task<IActionResult> ReloadXboxGame([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => xboxBL.ReloadXboxGame(gameId));
    }
}
