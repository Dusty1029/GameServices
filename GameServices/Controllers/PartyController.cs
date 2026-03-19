using CommonV2.Helpers.Controller;
using GameService.API.BusinessLogics.Implementations;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.PartyDtos;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PartyController(IControllerExecutor controllerExecutor,
        IPartyBL partyBL) : ControllerBase
    {
        [HttpGet]
        [Route("gages")]
        public Task<IActionResult> GetAllGages()
            => controllerExecutor.ExecuteAsync(this, partyBL.GetAllGages);

        [HttpPost]
        [Route("gages")]
        public Task<IActionResult> CreateGage([FromBody] CreateGageDto createGage)
            => controllerExecutor.ExecuteAsync(this, () => partyBL.CreateGage(createGage));


        [HttpGet]
        [Route("players")]
        public Task<IActionResult> GetAllPlayers()
            => controllerExecutor.ExecuteAsync(this, partyBL.GetAllPlayers);

        [HttpPost]
        [Route("players")]
        public Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto createPlayer)
            => controllerExecutor.ExecuteAsync(this, () => partyBL.CreatePlayer(createPlayer));

        [HttpGet]
        [Route("games")]
        public Task<IActionResult> GetAllGames()
            => controllerExecutor.ExecuteAsync(this, partyBL.GetAllGames);

        [HttpPost]
        [Route("games")]
        public Task<IActionResult> CreateGame([FromBody] CreatePartyGameDto createGame)
            => controllerExecutor.ExecuteAsync(this, () => partyBL.CreateGame(createGame));

        [HttpGet]
        public Task<IActionResult> GetAllParty()
            => controllerExecutor.ExecuteAsync(this, partyBL.GetAllParty);

        [HttpPost]
        public Task<IActionResult> CreateParty([FromBody] CreatePartyDto createParty)
            => controllerExecutor.ExecuteAsync(this, () => partyBL.CreateParty(createParty));

        [HttpDelete]
        public Task<IActionResult> Clear()
            => controllerExecutor.ExecuteAsync(this, partyBL.Clear);

        [HttpGet]
        [Route("{partyId}")]
        public Task<IActionResult> GetPartyById([FromRoute] Guid partyId) 
            => controllerExecutor.ExecuteAsync(this, () => partyBL.GetPartyById(partyId));

        [HttpPut]
        [Route("{partyId}/winningTeam/{winningTeamId}")]
        public Task<IActionResult> GetNextRound([FromRoute] Guid partyId, [FromRoute] Guid winningTeamId)
             => controllerExecutor.ExecuteAsync(this, () => partyBL.GetNextRound(partyId, winningTeamId));

        [HttpPut]
        [Route("{partyId}/cancel")]
        public Task<IActionResult> CancelPreviousRound([FromRoute] Guid partyId)
             => controllerExecutor.ExecuteAsync(this, () => partyBL.CancelPreviousRound(partyId));
    }
}
