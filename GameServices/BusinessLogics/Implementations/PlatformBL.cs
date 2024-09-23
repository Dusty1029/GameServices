using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PlatformBL(IPlatformRepository platformRepository) : IPlatformBL
    {
        public async Task<Guid> CreatePlatform(string platformName) => (await platformRepository.InsertAndSave(new() { Name = platformName })).Id;

        public Task<List<PlatformDto>> GetAllPlatforms() 
            => platformRepository.GetAllSelect(f => f.Select(p => new PlatformDto() { Id = p.Id, Name = p.Name }));

        
    }
}
