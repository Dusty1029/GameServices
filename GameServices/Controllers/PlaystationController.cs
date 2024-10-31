using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlaystationController(IControllerExecutor controllerExecutor, IPlaystationBL playstationBL) : ControllerBase
    {
        [HttpPut]
        [Route("token/{npsso}")]
        public Task<IActionResult> RefreshToken([FromRoute] string npsso)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.RefreshToken(npsso));

        [HttpGet]
        [Route("force/{forceReload}")]
        public Task<IActionResult> GetMissingSteamGames([FromRoute] bool forceReload)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.GetMissingPlaystationGames(forceReload));
        
        [HttpPost]
        public Task<IActionResult> AddPlaystationGame([FromBody] CreatePlaystationGameDto gamePlaystationDto)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.AddPlaystationGame(gamePlaystationDto));

        [HttpPost]
        [Route("ignore/{isIgnored}")]
        public Task<IActionResult> IgnorePlaystationGame([FromBody] PlaystationGameDto gamePlaystationDto, [FromRoute] bool isIgnored)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.IgnorePlaystationGame(gamePlaystationDto, isIgnored));

        [HttpPut]
        [Route("game/{gameDetailId}/reload")]
        public Task<IActionResult> ReloadPlaystationGame([FromRoute] Guid gameDetailId)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.ReloadPlaystationGame(gameDetailId));
    }
}
