using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class CategoryService(HttpClient httpClient) : ICategoryService
    {
        public Task<List<CategoryDto>> GetAllCategories() => httpClient.Get<List<CategoryDto>>();
    }
}
