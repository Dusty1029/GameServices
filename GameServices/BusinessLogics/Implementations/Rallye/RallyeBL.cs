using CommonV2.Models.Exceptions;
using GameService.API.BusinessLogics.Interfaces.Rallye;
using GameService.API.Extensions.Entities.Rallye;
using GameService.API.RallyeDtos;
using GameService.Infrastructure.Repositories.Interfaces.Rallye;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations.Rallye
{
    public class RallyeBL(IRallyeRepository rallyeRepository,
        IPlayerRepository playerRepository,
        ISpecialRepository specialRepository,
        ISpecialTimeRepository specialTimeRepository) : IRallyeBL
    {
        public async Task<Guid> CreateRallye(CreateRallyeDto createRallye)
        {
            var rallye = createRallye.ToEntity();
            rallye.Players = await playerRepository.Get(p => createRallye.PlayerIds.Contains(p.Id), noTracking: false);
            
            return (await rallyeRepository.InsertAndSave(rallye)).Id;
        }

        public async Task<Guid> CreateSpecial(Guid rallyeId, CreateSpecialDto createSpecial) => 
            (await specialRepository.InsertAndSave(createSpecial.ToEntity())).Id;

        public async Task<Guid> CreateSpecialTime(Guid rallyeId, Guid specialId, CreateSpecialTimeDto createSpecialTimeDto) =>
            (await specialTimeRepository.InsertAndSave(createSpecialTimeDto.ToEntity())).Id;

        public async Task<List<RallyeDto>> GetAllRallye() =>
            (await rallyeRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToSimpleDto()).ToList();

        public async Task<RallyeDto> GetRallyeById(Guid rallyeId)
        {
            var rallye = await rallyeRepository.Find(r => r.Id == rallyeId,
                    f => f.Include(r => r.Specials)!.ThenInclude(s => s.SpecialTimes)
                          .Include(r => r.Players)
                );

            return rallye is null ? throw new NotFoundException($"The rallye with id [{rallyeId}] was not found.") : rallye.ToDto();
        }
    }
}
