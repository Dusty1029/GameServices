using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Implementations;
using GameServices.API.BusinessLogics.Interfaces;
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
    }
}
