
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IXboxBL
    {
        Task<Guid> AddXboxGame(CreateXboxGameDto xboxGameDto);
        Task<List<XboxGameDto>> GetMissingXboxGames(bool forceReload);
        Task IgnoreXboxGame(XboxGameDto xboxGameDto, bool isIgnored);
        Task ReloadXboxGame(Guid gameId);
    }
}
