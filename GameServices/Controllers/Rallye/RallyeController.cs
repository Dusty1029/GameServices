using CommonV2.Helpers.Controller;
using GameService.API.BusinessLogics.Implementations;
using GameService.API.BusinessLogics.Interfaces.Rallye;
using GameService.API.RallyeDtos;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers.Rallye
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RallyeController(IControllerExecutor controllerExecutor, IRallyeBL rallyeBL) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAllRallye()
            => controllerExecutor.ExecuteAsync(this, rallyeBL.GetAllRallye);

        [HttpGet]
        [Route("{rallyeId}")]
        public Task<IActionResult> GetRallyeById([FromRoute] Guid rallyeId)
            => controllerExecutor.ExecuteAsync(this, () => rallyeBL.GetRallyeById(rallyeId));

        [HttpPost]
        public Task<IActionResult> CreateRallye([FromBody] CreateRallyeDto createRallye)
            => controllerExecutor.ExecuteAsync(this, () => rallyeBL.CreateRallye(createRallye));

        [HttpPost]
        [Route("{rallyeId}/special")]
        public Task<IActionResult> CreateSpecial([FromRoute] Guid rallyeId, [FromBody] CreateSpecialDto createSpecial)
            => controllerExecutor.ExecuteAsync(this, () => rallyeBL.CreateSpecial(rallyeId, createSpecial));

        [HttpPost]
        [Route("{rallyeId}/special/{specialId}/time")]
        public Task<IActionResult> CreateSpecialTime([FromRoute] Guid rallyeId, [FromRoute] Guid specialId, [FromBody] CreateSpecialTimeDto createSpecialTimeDto)
            => controllerExecutor.ExecuteAsync(this, () => rallyeBL.CreateSpecialTime(rallyeId, specialId, createSpecialTimeDto));



    }
}
