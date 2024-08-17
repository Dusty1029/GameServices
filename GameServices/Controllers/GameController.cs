using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IControllerExecutor _controllerExecutor;
        private readonly IGameBL _gameBL;

        public GameController(ILogger<GameController> logger,
            IControllerExecutor controllerExecutor,
            IGameBL gameBL)
        {
            _logger = logger;
            _controllerExecutor = controllerExecutor;
            _gameBL = gameBL;
        }

        [HttpPost]
        public Task<IActionResult> CreateGame([FromBody] GameDto createGameDto)
            => _controllerExecutor.ExecuteAsync(this, () => _gameBL.CreateGame(createGameDto));

        [HttpPut]
        [Route("{gameId}")]
        public Task<IActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameDto gameDto)
            => _controllerExecutor.ExecuteAsync(this, () => _gameBL.UpdateGame(gameId, gameDto));

        [HttpGet]
        [Route("{gameId}")]
        public Task<IActionResult> GetGameById([FromRoute] Guid gameId)
            => _controllerExecutor.ExecuteAsync(this, () => _gameBL.GetGameById(gameId));

        [HttpDelete]
        [Route("{gameId}")]
        public Task<IActionResult> DeleteGameById([FromRoute] Guid gameId)
            => _controllerExecutor.ExecuteAsync(this, () => _gameBL.DeleteGameById(gameId));

        [HttpPost]
        [Route("search")]
        public Task<IActionResult> SearchGame([FromBody] SearchGameDto searchGameDto)
            => _controllerExecutor.ExecuteAsync(this, () => _gameBL.SearchGame(searchGameDto));
    }
}
