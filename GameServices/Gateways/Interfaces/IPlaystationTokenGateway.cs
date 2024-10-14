namespace GameService.API.Gateways.Interfaces
{
    public interface IPlaystationTokenGateway
    {
        Task<string?> GetAuthenticationToken(string npsso);
    }
}
