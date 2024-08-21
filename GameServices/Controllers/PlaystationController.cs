using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Implementations;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos.PlaystationGateway;
using GameServices.API.Dtos.SteamGateway;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlaystationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IControllerExecutor _controllerExecutor;
        private readonly IPlaystationBL _playstationBL;

        public PlaystationController(ILogger<PlaystationController> logger, IControllerExecutor controllerExecutor, IPlaystationBL playstationBL)
        {
            _logger = logger;
            _controllerExecutor = controllerExecutor;
            _playstationBL = playstationBL;
        }

        [HttpPut]
        [Route("token/{npsso}")]
        public Task<IActionResult> RefreshToken([FromRoute] string npsso)
            => _controllerExecutor.ExecuteAsync(this, () => _playstationBL.RefreshToken(npsso));

        [HttpGet]
        public Task<IActionResult> GetMissingSteamGames()
            => _controllerExecutor.ExecuteAsync(this, () => _playstationBL.GetMissingPlaystationGames());

        [HttpPost]
        public Task<IActionResult> AddPlaystationGame([FromBody] GamePlaystationDto gamePlaystationDto)
            => _controllerExecutor.ExecuteAsync(this, () => _playstationBL.AddPlaystationGame(gamePlaystationDto));

        [HttpPost]
        [Route("ignore")]
        public Task<IActionResult> IgnorePlaystationGame([FromBody] GamePlaystationDto gameSteamDto)
            => _controllerExecutor.ExecuteAsync(this, () => _playstationBL.IgnorePlaystationGame(gameSteamDto));

        [HttpPut]
        [Route("game/{gameId}/reload")]
        public Task<IActionResult> ReloadPlaystationGame([FromRoute] Guid gameId)
            => _controllerExecutor.ExecuteAsync(this, () => _playstationBL.ReloadPlaystationGame(gameId));
    }
}
