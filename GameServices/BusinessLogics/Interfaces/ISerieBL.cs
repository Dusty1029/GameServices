
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISerieBL
    {
        Task<Guid> CreateSerie(CreateSerieDto createSerie);
        Task DeleteSerie(Guid id);
        Task<List<SimpleSerieDto>> GetAllSeries();
        Task<SerieDto> GetSerieById(Guid id);
        Task UpdateSerie(Guid id, CreateSerieDto createSerie);
    }
}
