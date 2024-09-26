using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class CategoryService(IGenericService genericService) : ICategoryService
    {
        private readonly string beginPath = "category";
        public Task<ApiResult<List<CategoryDto>>> GetAllCategories() => genericService.GetResult<List<CategoryDto>>(beginPath);
    }
}
