using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Extensions.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SerieBL(ISerieRepository serieRepository) : ISerieBL
    {
        public async Task<Guid> CreateSerie(CreateSerieDto createSerie)
        {
            if (await serieRepository.Exists(s => EF.Functions.Like(s.Name.ToLower(), $"%{createSerie.Serie.ToLower()}%")))
                throw new ArgumentException($"Serie with name [{createSerie.Serie}] already exist.");

            if (createSerie.ParentId.HasValue)
            {
                var parentExist = await serieRepository.Exists(s => s.Id == createSerie.ParentId);
                if (!parentExist)
                    throw new NotFoundException($"Serie with id [{createSerie.ParentId.Value}] was not found.");
            }
            
            var serie = createSerie.ToEntity();
            await serieRepository.InsertAndSave(serie);

            return serie.Id;
        }

        public async Task DeleteSerie(Guid id)
        {
            var serie = await serieRepository.Find(s => s.Id == id, include: f => f.Include(s => s.ChildrenSeries), noTracking: false)
                ?? throw new NotFoundException($"Serie with id [{id}] was not found.");

            if (serie.IsDefault)
                throw new ValidationException($"The serie with id [{id}] is a seed and can't be deleted.");

            await MarkChildrenAsDelete(serie);
            await serieRepository.DeleteAndSave(serie);
        }
        public async Task MarkChildrenAsDelete(SerieEntity serie)
        {
            if (serie.ChildrenSeries!.Count > 0)
            {
                foreach (var child in serie.ChildrenSeries!) 
                {
                    serieRepository.Delete(child);
                    var childWithChildren = await serieRepository.Find(s => s.Id == child.Id, include: f => f.Include(s => s.ChildrenSeries), noTracking: false);
                    await MarkChildrenAsDelete(childWithChildren!);
                }
            }
        }

        public Task<List<SimpleSerieDto>> GetAllSeries() =>
            serieRepository.GetAllSelect(f => f.Select(s => new SimpleSerieDto { Id = s.Id, Serie = s.Name, CanBeDeleted = !s.IsDefault }), orderBy: f => f.OrderBy(s => s.Name));

        public async Task<SerieDto> GetSerieByName(string name)
        {
            var serie = await serieRepository.Find(s => s.Name == name, f => f.Include(s => s.ParentSerie).Include(s => s.ChildrenSeries).Include(s => s.Games!.OrderBy(g => g.PlayOrder)))
                ?? throw new NotFoundException($"Serie with name [{name}] was not found.");

            return serie.ToDto();
        }

        public async Task UpdateSerie(Guid id, CreateSerieDto createSerie)
        {
            var serie = await serieRepository.Find(s => s.Id == id, f => f.Include(s => s.Games), noTracking: false)
                ?? throw new NotFoundException($"Serie with id [{id}] was not found.");

            createSerie.ToEntity(serie);
            await serieRepository.SaveChanges();
        }

        public async Task<List<SearchGameItemDto>> GetSeriesWithGames()
        {
            var series = await serieRepository.Get(
                s => s.Games!.Count > 0,
                f => f.Include(s => s.Games!).ThenInclude(g => g.GameDetails!).ThenInclude(gd => gd.Platform)
                      .Include(s => s.Games!).ThenInclude(g => g.Categories),
                f => f.OrderByDescending(s => !s.IsDefault)
                      .ThenByDescending(s => s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.Started.GetOrder()))
                      .ThenByDescending(s => s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.Finished.GetOrder()) &&
                                             (s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.NotStarted.GetOrder()) ||
                                             s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.ToBuy.GetOrder())))
                      .ThenByDescending(s => s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.TotalyFinished.GetOrder()) &&
                                             (s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.NotStarted.GetOrder()) ||
                                             s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.ToBuy.GetOrder())))
                      .ThenByDescending(s => s.Games!.All(g => g.StatusOrder == GameDetailStatusEnumEntity.Finished.GetOrder()))
                      .ThenByDescending(s => s.Games!.All(g => g.StatusOrder == GameDetailStatusEnumEntity.NotStarted.GetOrder()))
                      .ThenByDescending(s => s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.NotStarted.GetOrder()) &&
                                             s.Games!.Any(g => g.StatusOrder == GameDetailStatusEnumEntity.ToBuy.GetOrder()))
                      .ThenByDescending(s => s.Games!.All(g => g.StatusOrder == GameDetailStatusEnumEntity.ToBuy.GetOrder()))
                      .ThenByDescending(s => s.Games!.All(g => g.StatusOrder == GameDetailStatusEnumEntity.TotalyFinished.GetOrder()))
                      .ThenBy(s => s.Name));
            return series.SelectMany(s => s.Games!.OrderBy(g => g.PlayOrder).ThenBy(g => g.Name)).Select(g => g.ToSearchItemDto()).ToList();
        }
    } 
}
