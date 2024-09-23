using CommonV2.Helpers.Controller;
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
        public Task<IActionResult> GetAllCategories()
            => controllerExecutor.ExecuteAsync(this, () => platformBL.GetAllPlatforms());

        [HttpPost]
        public Task<IActionResult> CreatePlatform([FromBody] string platformName)
            => controllerExecutor.ExecuteAsync(this, () => platformBL.CreatePlatform(platformName));
    }
}
