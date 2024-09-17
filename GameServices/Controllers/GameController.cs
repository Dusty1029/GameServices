using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GameController(IControllerExecutor controllerExecutor,
        IGameBL gameBL) : ControllerBase
    {
        /*[HttpPost]
        public Task<IActionResult> CreateGame([FromBody] GameDto createGameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.CreateGame(createGameDto));

        [HttpPut]
        [Route("{gameId}")]
        public Task<IActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameDto gameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.UpdateGame(gameId, gameDto));

        [HttpGet]
        [Route("{gameId}")]
        public Task<IActionResult> GetGameById([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.GetGameById(gameId));*/

        [HttpDelete]
        [Route("{gameId}")]
        public Task<IActionResult> DeleteGameById([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.DeleteGameById(gameId));

        [HttpPost]
        [Route("search")]
        public Task<IActionResult> SearchGame([FromBody] SearchGameDto searchGameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.SearchGame(searchGameDto));
    }
}
