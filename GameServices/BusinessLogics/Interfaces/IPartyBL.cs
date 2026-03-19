
using GameService.API.PartyDtos;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPartyBL
    {
        Task<PartyDto> CancelPreviousRound(Guid partyId);
        Task Clear();
        Task<Guid> CreateGage(CreateGageDto createGage);
        Task<Guid> CreateGame(CreatePartyGameDto createGame);
        Task<Guid> CreateParty(CreatePartyDto createParty);
        Task<Guid> CreatePlayer(CreatePlayerDto createPlayer);
        Task<List<GageDto>> GetAllGages();
        Task<List<PartyGameDto>> GetAllGames();
        Task<List<SimplePartyDto>> GetAllParty();
        Task<List<PlayerDto>> GetAllPlayers();
        Task<PartyDto> GetNextRound(Guid partyId, Guid winningTeamId);
        Task<PartyDto> GetPartyById(Guid partyId);
    }
}
