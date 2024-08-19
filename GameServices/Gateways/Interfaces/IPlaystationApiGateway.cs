using GameServices.API.Dtos.PlaystationGateway;

namespace GameServices.API.Gateways.Interfaces
{
    public interface IPlaystationApiGateway
    {
        Task<string?> GetAuthenticationToken(string npsso);
        Task<List<GamePlaystationDto>?> GetPlaystationGames(string token);
    }
}
