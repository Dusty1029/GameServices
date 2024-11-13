using Game.Dto.Games;
using Game.Dto.Series;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISerieService
    {
        Task<ApiResult<Guid>> CreateSerie(CancellationToken cancellationToken, CreateSerieDto createSerie);
        Task<ApiResult> DeleteSerie(CancellationToken cancellationToken, Guid id);
        Task<ApiResult<List<SimpleSerieDto>>> GetAllSeries(CancellationToken cancellationToken);
        Task<ApiResult<SerieDto>> GetSerieByName(CancellationToken cancellationToken, string name);
        Task<ApiResult> UpdateSerie(CancellationToken cancellationToken, Guid id, CreateSerieDto createSerie);
        Task<ApiResult<List<SearchGameItemDto>>> GetSeriesWithGames(CancellationToken cancellationToken);
    }
}