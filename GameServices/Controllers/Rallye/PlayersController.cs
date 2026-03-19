using CommonV2.Helpers.Controller;
using GameService.API.BusinessLogics.Interfaces.Rallye;
using GameService.API.RallyeDtos;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers.Rallye
{
    [ApiController]
    [Route("api/v1/rallye/[controller]")]
    public class PlayersController(IControllerExecutor controllerExecutor,
        IPlayerBL playerBL) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAllPlayers()
            => controllerExecutor.ExecuteAsync(this, playerBL.GetAllPlayers);

        [HttpPost]
        public Task<IActionResult> CreatePlayer([FromBody] CreateRallyePlayerDto createPlayer)
            => controllerExecutor.ExecuteAsync(this, () => playerBL.CreatePlayer(createPlayer));
    }
}
