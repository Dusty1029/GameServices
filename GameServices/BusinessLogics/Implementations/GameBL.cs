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

namespace GameService.API.BusinessLogics.Implementations
{
    public class GameBL(IGameRepository gameRepository, 
        IPlatformRepository platformRepository,
        ICategoryRepository categoryRepository,
        IGameDetailRepository gameDetailRepository) : IGameBL
    {
        public async Task<Guid> CreateGame(CreateGameDto createGameDto)
        {
            if (!await platformRepository.Exists(p => p.Id == createGameDto.Platform!.Id)) 
                throw new NotFoundException($"The platform with id [{createGameDto.Platform!.Id}] was not found.");

            var categories = new List<CategoryEntity>();
            if (createGameDto.Categories is not null)
            {
                var categoryIds = createGameDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            var game = await gameRepository.CreateGame(createGameDto.ToEntity(), categories);
            return game.Id;
        }

        public async Task DeleteGameById(Guid gameDetailId)
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
            var game = await gameDetailRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Game).ThenInclude(g => g.Categories).Include(g => g.Achievements).Include(g => g.Platform));
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            return game.ToDto();
        }

        public async Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto)
        {
            var gamesSearched = await gameDetailRepository.Search(
                    searchGameDto.Size,
                    searchGameDto.Page,
                    BuildSearchPredicate(searchGameDto),
                    include: query => query.Include(g => g.Game!.Categories!.OrderBy(c => c.Name)).Include(g => g.Platform),
                    orderBy: query => query.OrderBy(g => g.Game!.Name).ThenBy(g => g.Platform)
                );

            return new()
            {
                TotalItems = gamesSearched.TotalItems,
                Items = gamesSearched.Items.Select(i => i.ToDto()).ToList()
            };
        }
        private static Expression<Func<GameDetailEntity, bool>> BuildSearchPredicate(SearchGameDto searchGameDto)
        {
            var predicate = PredicateBuilder.New<GameDetailEntity>();
            predicate = predicate.And(g => g.Game!.Name.ToLower().Contains(searchGameDto.Name.ToLower()));
            predicate = predicate.And(g => !searchGameDto.PlatformId.HasValue || g.PlatformId == searchGameDto.PlatformId);

            if (searchGameDto.CategoriesId is not null)
            {
                foreach (var categoryId in searchGameDto.CategoriesId)
                {
                    predicate = predicate.And(g => g.Game!.Categories!.Select(c => c.Id).Contains(categoryId));
                }
            }

            return predicate;
        }

        /*public async Task UpdateGame(Guid gameId, GameDto gameDto)
        {
            var game = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories), noTracking: false);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            gameDto.ToEntity(game);

            var actualCategories = gameDto.Categories?.Select(c => c.ToEntity()).ToList() ?? new List<CategoryEntity>();
            game.Categories!.RemoveAll(c => !actualCategories.Any(ac => ac.Id == c.Id));
            game.Categories!.AddRange(actualCategories.Where(ac => !game.Categories!.Any(c => c.Id == ac.Id)));

            await gameRepository.SaveChanges();
        }*/
    }
}
