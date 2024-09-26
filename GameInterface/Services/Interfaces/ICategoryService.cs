using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResult<List<CategoryDto>>> GetAllCategories();
    }
}
