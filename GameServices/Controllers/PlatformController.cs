using CommonV2.Helpers.Controller;
using GameService.API.BusinessLogics.Implementations;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlatformController(IControllerExecutor controllerExecutor,
        IPlatformBL platformBL) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAllPlatforms()
            => controllerExecutor.ExecuteAsync(this, () => platformBL.GetAllPlatforms());

        [HttpPost]
        public Task<IActionResult> CreatePlatform([FromBody] string platformName)
            => controllerExecutor.ExecuteAsync(this, () => platformBL.CreatePlatform(platformName));

        [HttpPut]
        [Route("{id}")]
        public Task<IActionResult> CreatePlatform([FromRoute] Guid id, [FromBody] string platformName)
            => controllerExecutor.ExecuteAsync(this, () => platformBL.UpdatePlatform(id, platformName));

        [HttpDelete]
        [Route("{id}")]
        public Task<IActionResult> DeletePlatform([FromRoute] Guid id)
            => controllerExecutor.ExecuteAsync(this, () => platformBL.DeletePlatform(id));
    }
}
