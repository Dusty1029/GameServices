using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos.SteamGateway;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SteamController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IControllerExecutor _controllerExecutor;
        private readonly ISteamBL _steamBL;

        public SteamController(ILogger<SteamController> logger,
            IControllerExecutor controllerExecutor,
            ISteamBL steamBL)
        {
            _logger = logger;
            _controllerExecutor = controllerExecutor;
            _steamBL = steamBL;
        }

        [HttpGet]
        public Task<IActionResult> GetMissingSteamGames()
            => _controllerExecutor.ExecuteAsync(this, () => _steamBL.GetMissingSteamGames());

        [HttpPost]
        public Task<IActionResult> AddSteamGame([FromBody] GameSteamDto gameSteamDto)
            => _controllerExecutor.ExecuteAsync(this, () => _steamBL.AddSteamGame(gameSteamDto));

        [HttpPost]
        [Route("ignore")]
        public Task<IActionResult> IgnoreSteamGame([FromBody] GameSteamDto gameSteamDto)
            => _controllerExecutor.ExecuteAsync(this, () => _steamBL.IgnoreSteamGame(gameSteamDto));

        [HttpPut]
        [Route("game/{gameId}/reload")]
        public Task<IActionResult> ReloadSteamGame([FromRoute] Guid gameId)
            => _controllerExecutor.ExecuteAsync(this, () => _steamBL.ReloadSteamGame(gameId));
    }
}
