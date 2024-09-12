using CommonV2.Helpers.Controller;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Models.PlaystationGateway;
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
        public Task<IActionResult> GetMissingSteamGames()
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.GetMissingPlaystationGames());

        [HttpPost]
        public Task<IActionResult> AddPlaystationGame([FromBody] GamePlaystation gamePlaystationDto)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.AddPlaystationGame(gamePlaystationDto));

        [HttpPost]
        [Route("ignore")]
        public Task<IActionResult> IgnorePlaystationGame([FromBody] GamePlaystation gameSteamDto)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.IgnorePlaystationGame(gameSteamDto));

        [HttpPut]
        [Route("game/{gameId}/reload")]
        public Task<IActionResult> ReloadPlaystationGame([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => playstationBL.ReloadPlaystationGame(gameId));
    }
}
