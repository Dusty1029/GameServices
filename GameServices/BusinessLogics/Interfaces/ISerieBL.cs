
using CommonV2.Models;
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISerieBL
    {
        Task<Guid> CreateSerie(CreateSerieDto createSerie);
        Task DeleteSerie(Guid id);
        Task<List<SimpleSerieDto>> GetAllSeries();
        Task<SerieDto> GetSerieById(Guid id);
        Task<List<SerieDto>> GetSeriesWithGames();
        Task UpdateSerie(Guid id, CreateSerieDto createSerie);
    }
}
