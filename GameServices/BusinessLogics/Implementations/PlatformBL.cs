using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PlatformBL(IPlatformRepository platformRepository) : IPlatformBL
    {
        public async Task<Guid> CreatePlatform(string platformName) => (await platformRepository.InsertAndSave(new() { Name = platformName })).Id;

        public async Task DeletePlatform(Guid id)
        {
            var platformEntity = await platformRepository.Find(p => p.Id == id && !p.IsSeed, noTracking: false) ??
                throw new NotFoundException($"The platform with id {id} was not found.");

            await platformRepository.DeleteAndSave(platformEntity);
        }

        public Task<List<PlatformDto>> GetAllPlatforms() 
            => platformRepository.GetAllSelect(f => f.Select(p => new PlatformDto() { Id = p.Id, Name = p.Name, CanBeDeleted = !p.IsSeed }), orderBy: f => f.OrderBy(p => p.Name));

        public async Task UpdatePlatform(Guid id, string platformName)
        {
            var platformEntity = await platformRepository.Find(p => p.Id == id && !p.IsSeed, noTracking: false) ?? 
                throw new NotFoundException($"The platform with id {id} was not found.");

            platformEntity.Name = platformName;
            await platformRepository.SaveChanges();
        }
    }
}
