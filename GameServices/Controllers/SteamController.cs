using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SteamController(IControllerExecutor controllerExecutor,
        ISteamBL steamBL) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetMissingSteamGames()
            => controllerExecutor.ExecuteAsync(this, () => steamBL.GetMissingSteamGames());

        [HttpPost]
        public Task<IActionResult> AddSteamGame([FromBody] SteamGameDto steamGameDto)
            => controllerExecutor.ExecuteAsync(this, () => steamBL.AddSteamGame(steamGameDto));

        [HttpPost]
        [Route("ignore")]
        public Task<IActionResult> IgnoreSteamGame([FromBody] SteamGameDto steamGameDto)
            => controllerExecutor.ExecuteAsync(this, () => steamBL.IgnoreSteamGame(steamGameDto));

        [HttpPut]
        [Route("game/{gameId}/reload")]
        public Task<IActionResult> ReloadSteamGame([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => steamBL.ReloadSteamGame(gameId));
    }
}
