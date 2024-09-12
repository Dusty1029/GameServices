using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AchievementController(IControllerExecutor controllerExecutor,
        IAchievementBL achievementBL) : ControllerBase
    {

        [HttpPost]
        public Task<IActionResult> CreateAchievement([FromBody] AchievementDto achievementDto)
            => controllerExecutor.ExecuteAsync(this, () => achievementBL.CreateAchievement(achievementDto));

        [HttpPut]
        [Route("{achievementId}")]
        public Task<IActionResult> UpdateAchievement([FromRoute] Guid achievementId, [FromBody] AchievementDto achievementDto)
            => controllerExecutor.ExecuteAsync(this, () => achievementBL.UpdateAchievement(achievementId, achievementDto));

        [HttpDelete]
        [Route("{achievementId}")]
        public Task<IActionResult> DeleteAchievement([FromRoute] Guid achievementId)
            => controllerExecutor.ExecuteAsync(this, () => achievementBL.DeleteAchievement(achievementId));

        [HttpPut]
        [Route("{achievementId}/achieved/{achieved}")]
        public Task<IActionResult> UpdateAchievedAchievement([FromRoute] Guid achievementId, [FromRoute] bool achieved)
            => controllerExecutor.ExecuteAsync(this, () => achievementBL.UpdateAchievedAchievement(achievementId, achieved));
    }
}
