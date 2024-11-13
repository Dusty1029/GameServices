using Game.Dto.Games;
using Game.Dto.Series;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class SerieService(IGenericService genericService) : ISerieService
    {
        private readonly string beginPath = "serie";
        public Task<ApiResult<Guid>> CreateSerie(CancellationToken cancellationToken, CreateSerieDto createSerie) =>
            genericService.PostResult<Guid>(cancellationToken, createSerie, beginPath);

        public Task<ApiResult> DeleteSerie(CancellationToken cancellationToken, Guid id) =>
            genericService.DeleteResult(cancellationToken, $"{beginPath}/{id}");

        public Task<ApiResult<List<SimpleSerieDto>>> GetAllSeries(CancellationToken cancellationToken) =>
            genericService.GetResult<List<SimpleSerieDto>>(cancellationToken, beginPath);

        public Task<ApiResult<SerieDto>> GetSerieByName(CancellationToken cancellationToken, string name) =>
            genericService.GetResult<SerieDto>(cancellationToken, $"{beginPath}/{name}");

        public Task<ApiResult<List<SearchGameItemDto>>> GetSeriesWithGames(CancellationToken cancellationToken) =>
            genericService.GetResult<List<SearchGameItemDto>>(cancellationToken, $"{beginPath}/games");

        public Task<ApiResult> UpdateSerie(CancellationToken cancellationToken, Guid id, CreateSerieDto createSerie) =>
            genericService.PutResult(cancellationToken, createSerie, $"{beginPath}/{id}");
    }
}
