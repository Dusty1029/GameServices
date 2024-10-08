using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SerieBL(ISerieRepository serieRepository,
        IGameRepository gameRepository) : ISerieBL
    {
        public async Task<Guid> CreateSerie(CreateSerieDto createSerie)
        {
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
            serieRepository.GetAllSelect(f => f.Select(s => new SimpleSerieDto { Id = s.Id, Serie = s.Name }), orderBy: f => f.OrderBy(s => s.Name));

        public async Task<SerieDto> GetSerieById(Guid id)
        {
            var serie = await serieRepository.Find(s => s.Id == id, f => f.Include(s => s.ParentSerie).Include(s => s.ChildrenSeries).Include(s => s.Games))
                ?? throw new NotFoundException($"Serie with id [{id}] was not found.");

            return serie.ToDto();
        }

        public async Task UpdateSerie(Guid id, CreateSerieDto createSerie)
        {
            var serie = await serieRepository.Find(s => s.Id == id, noTracking: false)
                ?? throw new NotFoundException($"Serie with id [{id}] was not found.");

            createSerie.ToEntity(serie);
            await serieRepository.SaveChanges();
        }

        public async Task<List<SerieDto>> GetSeriesWithGames()
        {
            var games = await gameRepository.GetAll(f => f.Include(g => g.Serie));
            var series = games.Where(g => g.Serie != null).Select(g => g.Serie).DistinctBy(s => s!.Id).ToList();
            var serieDtos = series.Select(s => s!.ToDto(games.Where(g => g.SerieId == s!.Id).ToList())).ToList();
            serieDtos.Add(new() 
            { 
                Serie = "Pas de série",
                Games = games.Where(g => g.Serie == null).Select(g => g.ToSimpleDto()).ToList()
            });
            return serieDtos;
        }
    } 
}
