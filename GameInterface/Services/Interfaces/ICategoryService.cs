using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
