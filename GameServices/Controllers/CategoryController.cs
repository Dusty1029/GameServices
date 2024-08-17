using CommonV2.Helpers.Controller;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IControllerExecutor _controllerExecutor;
        private readonly ICategoryBL _categoryBL;

        public CategoryController(ILogger<CategoryController> logger,
            IControllerExecutor controllerExecutor,
            ICategoryBL categoryBL)
        {
            _logger = logger;
            _controllerExecutor = controllerExecutor;
            _categoryBL = categoryBL;
        }

        [HttpGet]
        public Task<IActionResult> GetAllCategories()
            => _controllerExecutor.ExecuteAsync(this, () => _categoryBL.GetAllCategories());

        [HttpPost]
        public Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
            => _controllerExecutor.ExecuteAsync(this, () => _categoryBL.CreateCategory(category));

        [HttpPut]
        [Route("{categoryId}")]
        public Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] CategoryDto category)
            => _controllerExecutor.ExecuteAsync(this, () => _categoryBL.UpdateCategory(categoryId, category));

        [HttpDelete]
        [Route("{categoryId}")]
        public Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
            => _controllerExecutor.ExecuteAsync(this, () => _categoryBL.DeleteCategory(categoryId));


    }
}
