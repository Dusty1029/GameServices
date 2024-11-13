using CommonV2.Helpers.Controller;
using Game.Dto.Goal;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GoalController(IControllerExecutor controllerExecutor,
        IGoalBL goalBL) : ControllerBase
    {
        [HttpPost]
        [Route("gameDetail/{gameDetailId}")]
        public Task<IActionResult> CreateGoal([FromRoute] Guid gameDetailId, [FromBody] CreateGoalDto createGoalDto)
            => controllerExecutor.ExecuteAsync(this, () => goalBL.CreateGoal(gameDetailId, createGoalDto));

        [HttpPut]
        [Route("{goalId}/achieved/{achieved}")]
        public Task<IActionResult> UpdateAchievedGoal([FromRoute] Guid goalId, [FromRoute] bool achieved)
            => controllerExecutor.ExecuteAsync(this, () => goalBL.UpdateAchievedGoal(goalId, achieved));
    }
}
