using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AchievementController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IControllerExecutor _controllerExecutor;
        private readonly IAchievementBL _achievementBL;

        public AchievementController(ILogger<AchievementController> logger,
            IControllerExecutor controllerExecutor,
            IAchievementBL achievementBL)
        {
            _logger = logger;
            _controllerExecutor = controllerExecutor;
            _achievementBL = achievementBL;
        }

        [HttpPost]
        public Task<IActionResult> CreateAchievement([FromBody] AchievementDto achievementDto)
            => _controllerExecutor.ExecuteAsync(this, () => _achievementBL.CreateAchievement(achievementDto));

        [HttpPut]
        [Route("{achievementId}")]
        public Task<IActionResult> UpdateAchievement([FromRoute] Guid achievementId, [FromBody] AchievementDto achievementDto)
            => _controllerExecutor.ExecuteAsync(this, () => _achievementBL.UpdateAchievement(achievementId, achievementDto));

        [HttpDelete]
        [Route("{achievementId}")]
        public Task<IActionResult> DeleteAchievement([FromRoute] Guid achievementId)
            => _controllerExecutor.ExecuteAsync(this, () => _achievementBL.DeleteAchievement(achievementId));

        [HttpPut]
        [Route("{achievementId}/achieved/{achieved}")]
        public Task<IActionResult> UpdateAchievedAchievement([FromRoute] Guid achievementId, [FromRoute] bool achieved)
            => _controllerExecutor.ExecuteAsync(this, () => _achievementBL.UpdateAchievedAchievement(achievementId, achieved));
    }
}
