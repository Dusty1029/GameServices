using CommonV2.Helpers.Controller;
using Game.Dto;
using Game.Dto.Enums;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GameController(IControllerExecutor controllerExecutor,
        IGameBL gameBL) : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.CreateGame(createGameDto));

        [HttpPost]
        [Route("{gameId}/platform/{platformId}")]
        public Task<IActionResult> AddPlatformToAGame([FromRoute] Guid gameId, [FromRoute] Guid platformId)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.AddPlatformToAGame(gameId, platformId));

        [HttpPut]
        [Route("{gameId}")]
        public Task<IActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] UpdateGameDto gameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.UpdateGame(gameId, gameDto));

        [HttpGet]
        [Route("{gameId}")]
        public Task<IActionResult> GetGameById([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.GetGameById(gameId));

        [HttpDelete]
        [Route("{gameId}")]
        public Task<IActionResult> DeleteGameById([FromRoute] Guid gameId)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.DeleteGameByGameDetailId(gameId));

        [HttpPost]
        [Route("search")]
        public Task<IActionResult> SearchGame([FromBody] SearchGameDto searchGameDto)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.SearchGame(searchGameDto));

        [HttpPost]
        [Route("search/{gameSearched}")]
        public Task<IActionResult> SearchSimpleGame([FromRoute] string gameSearched, [FromBody] PlatformEnumDto? ignoredPlatform)
            => controllerExecutor.ExecuteAsync(this, () => gameBL.SearchSimpleGame(gameSearched, ignoredPlatform));
    }
}
