
using CommonV2.Models;
using Game.Dto.Games;
using Game.Dto.Series;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISerieBL
    {
        Task<Guid> CreateSerie(CreateSerieDto createSerie);
        Task DeleteSerie(Guid id);
        Task<List<SimpleSerieDto>> GetAllSeries();
        Task<SerieDto> GetSerieByName(string name);
        Task<List<SearchGameItemDto>> GetSeriesWithGames();
        Task UpdateSerie(Guid id, CreateSerieDto createSerie);
    }
}
