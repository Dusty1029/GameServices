using GameService.API.RallyeDtos;

namespace GameService.API.BusinessLogics.Interfaces.Rallye
{
    public interface IRallyeBL
    {
        Task<Guid> CreateRallye(CreateRallyeDto createRallye);
        Task<Guid> CreateSpecial(Guid rallyeId, CreateSpecialDto createSpecial);
        Task<Guid> CreateSpecialTime(Guid rallyeId, Guid specialId, CreateSpecialTimeDto createSpecialTimeDto);
        Task<List<RallyeDto>> GetAllRallye();
        Task<RallyeDto> GetRallyeById(Guid rallyeId);
    }
}
