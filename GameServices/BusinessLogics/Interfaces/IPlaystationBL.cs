using GameServices.API.Dtos.PlaystationGateway;

namespace GameServices.API.BusinessLogics.Interfaces
{
    public interface IPlaystationBL
    {
        Task<List<GamePlaystationDto>?> GetMissingPlaystationGames();
        Task RefreshToken(string npsso);
    }
}
