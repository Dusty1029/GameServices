using GameServices.API.Dtos.PlaystationGateway;

namespace GameServices.API.BusinessLogics.Interfaces
{
    public interface IPlaystationBL
    {
        Task<Guid> AddPlaystationGame(GamePlaystationDto gamePlaystationDto);
        Task<List<GamePlaystationDto>?> GetMissingPlaystationGames();
        Task RefreshToken(string npsso);
    }
}
