using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(IControllerExecutor controllerExecutor,
        ICategoryBL categoryBL) : ControllerBase
    {

        [HttpGet]
        public Task<IActionResult> GetAllCategories()
            => controllerExecutor.ExecuteAsync(this, () => categoryBL.GetAllCategories());

        [HttpPost]
        public Task<IActionResult> CreateCategory([FromBody] string category)
            => controllerExecutor.ExecuteAsync(this, () => categoryBL.CreateCategory(category));

        [HttpPut]
        [Route("{categoryId}")]
        public Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] string category)
            => controllerExecutor.ExecuteAsync(this, () => categoryBL.UpdateCategory(categoryId, category));

        [HttpDelete]
        [Route("{categoryId}")]
        public Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
            => controllerExecutor.ExecuteAsync(this, () => categoryBL.DeleteCategory(categoryId));


    }
}
