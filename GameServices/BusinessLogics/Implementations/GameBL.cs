using CommonV2.Models;
using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Game.Dto.Enums;
using GameService.API.Extensions.Entities.Enums;

namespace GameService.API.BusinessLogics.Implementations
{
    public class GameBL(IGameRepository gameRepository, 
        IPlatformRepository platformRepository,
        ICategoryRepository categoryRepository,
        IGameDetailRepository gameDetailRepository,
        ISerieRepository serieRepository) : IGameBL
    {
        public async Task<Guid> CreateGame(CreateGameDto createGameDto)
        {
            if (!await platformRepository.Exists(p => p.Id == createGameDto.Platform!.Id)) 
                throw new NotFoundException($"The platform with id [{createGameDto.Platform!.Id}] was not found.");

            if (createGameDto.Serie is not null && !await serieRepository.Exists(s => s.Id == createGameDto.Serie.Id))
                throw new NotFoundException($"The serie with id [{createGameDto.Serie.Id}] was not found.");

            var categories = new List<CategoryEntity>();
            if (createGameDto.Categories is not null)
            {
                var categoryIds = createGameDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            var gameEntity = createGameDto.ToEntity();
            if (gameEntity.SerieId is null)
            {
                var defaultSerie = await serieRepository.FindDefaultSerie();
                gameEntity.SerieId = defaultSerie.Id;
            }

            await gameRepository.CreateGame(gameEntity, categories);
            return gameEntity.Id;
        }

        public async Task DeleteGameByGameDetailId(Guid gameDetailId)
        {
            var game = await gameRepository.Find(g => g.GameDetails!.Any(gd => gd.Id == gameDetailId), include: f => f.Include(g => g.GameDetails), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameDetailId}] was not found.");

            if (game.GameDetails!.Count == 1) 
            {
                await gameRepository.DeleteAndSave(game);
            }
            else
            {
                await gameDetailRepository.DeleteAndSave(game.GameDetails!.First(gd => gd.Id == gameDetailId));
            }
        }

        public async Task<GameDto> GetGameById(Guid gameId)
        {
            var game = await gameRepository.Find(g => g.Id == gameId,
                    f => f.Include(g => g.Categories!.OrderBy(c => c.Name))
                          .Include(g => g.Serie)
                          .Include(g => g.GameDetails)!.ThenInclude(gd => gd.Platform)
                          .Include(g => g.GameDetails)!.ThenInclude(gd => gd.Achievements!.OrderByDescending(a => a.Percentage).ThenBy(a => a.Name))
                );

            return game is null ? throw new NotFoundException($"The game with id [{gameId}] was not found.") : game.ToDto();
        }

        public async Task<PaginationResult<SearchGameItemDto>> SearchGame(SearchGameDto searchGameDto)
        {
            var gamesSearched = await gameRepository.Search(
                    searchGameDto.Size,
                    searchGameDto.Page,
                    BuildSearchPredicate(searchGameDto),
                    include: query => query.Include(g => g.Categories!.OrderBy(c => c.Name)).Include(g => g.GameDetails)!.ThenInclude(gd => gd.Platform).Include(g => g.Serie),
                    orderBy: query => query.OrderBy(g => g.Name)
                );

            return new()
            {
                TotalItems = gamesSearched.TotalItems,
                Items = gamesSearched.Items.Select(i => i.ToSearchItemDto()).ToList()
            };
        }
        private static Expression<Func<GameEntity, bool>> BuildSearchPredicate(SearchGameDto searchGameDto)
        {
            var predicate = PredicateBuilder.New<GameEntity>();
            predicate = predicate.And(g => g.Name.ToLower().Contains(searchGameDto.Name.ToLower()));
            predicate = predicate.And(g => !searchGameDto.PlatformId.HasValue || g.GameDetails!.Select(gd => gd.PlatformId).Contains(searchGameDto.PlatformId.Value));
            predicate = predicate.And(g => !searchGameDto.SerieId.HasValue || g.SerieId == searchGameDto.SerieId);

            if (searchGameDto.CategoriesId is not null)
            {
                foreach (var categoryId in searchGameDto.CategoriesId)
                {
                    predicate = predicate.And(g => g.Categories!.Select(c => c.Id).Contains(categoryId));
                }
            }

            return predicate;
        }

        public async Task UpdateGame(Guid gameId, UpdateGameDto gameDto)
        {
            var gameEntity = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories).Include(g => g.GameDetails), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            gameDto.ToEntity(gameEntity);

            var categories = new List<CategoryEntity>();
            if (gameDto.Categories is not null)
            {
                var categoryIds = gameDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            gameEntity.Categories!.RemoveAll(c => !categories.Any(ac => ac.Id == c.Id));
            gameEntity.Categories!.AddRange(categories.Where(ac => !gameEntity.Categories!.Any(c => c.Id == ac.Id)));

            if (gameEntity.SerieId is null)
            {
                var defaultSerie = await serieRepository.FindDefaultSerie();
                gameEntity.SerieId = defaultSerie.Id;
            }

            await gameRepository.SaveChanges();
        }

        public async Task<Guid> AddPlatformToAGame(Guid gameId, Guid platformId)
        {
            var game = await gameRepository.Find(g => g.Id == gameId) ??
                throw new NotFoundException($"The game with id [{gameId}] was not found.");
            var platform = await platformRepository.Find(p => p.Id == platformId) ??
                throw new NotFoundException($"The platform with id [{platformId}] was not found.");

            var gameDetailEntity = new GameDetailEntity
            {
                GameId = gameId,
                PlatformId = platformId
            };

            await gameDetailRepository.InsertAndSave(gameDetailEntity);

            return gameDetailEntity.Id;
        }

        public async Task<List<SimpleGameDto>> SearchSimpleGame(string gameSearched, PlatformEnumDto? ignoredPlatform)
        {
            if (gameSearched.Length < 1)
                return [];
            var ignoredPlatformEnum = ignoredPlatform?.ToEntity();

            return await gameRepository.GetSelect(f => f.Select(g => new SimpleGameDto { Id = g.Id, Name = g.Name }),
                g => EF.Functions.Like(g.Name.ToLower(), $"%{gameSearched.ToLower()}%") && 
                (!ignoredPlatformEnum.HasValue || !g.GameDetails!.Any(gd => gd.Platform!.PlatformEnum == ignoredPlatformEnum))
            );
        }
    }
}
