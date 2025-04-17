namespace GameService.API.Gateways.Interfaces
{
    public interface IHowLongToBeatGateway
    {
        Task<Models.HowLongToBeatGateway.Game?> FindHowLongToBeatGame(string game);
    }
}
