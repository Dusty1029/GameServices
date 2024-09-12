using CommonV2.Models;
using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Extensions.Entities.Enums;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameService.API.BusinessLogics.Implementations
{
    public class GameBL(IGameRepository gameRepository) : IGameBL
    {
        public async Task<Guid> CreateGame(GameDto createGameDto)
        {
            var game = await gameRepository.CreateGame(createGameDto.ToEntity(), createGameDto.Categories?.Select(c => c.ToEntity()).ToList());
            return game.Id;
        }

        public async Task DeleteGameById(Guid gameId)
        {
            var game = await gameRepository.Find(g => g.Id == gameId);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            await gameRepository.DeleteAndSave(game);
        }

        public async Task<GameDto> GetGameById(Guid gameId)
        {
            var game = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories).Include(g => g.Achievements));
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            return game.ToDto();
        }

        public async Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto)
        {
            var gamesSearched = await gameRepository.Search(
                    searchGameDto.Size,
                    searchGameDto.Page,
                    BuildSearchPredicate(searchGameDto),
                    include: query => query.Include(g => g.Categories!.OrderBy(c => c.Name)),
                    orderBy: query => query.OrderBy(g => g.Name).ThenBy(g => g.Platform)
                );

            return new()
            {
                TotalItems = gamesSearched.TotalItems,
                Items = gamesSearched.Items.Select(i => i.ToDto()).ToList()
            };
        }
        private static Expression<Func<GameEntity, bool>> BuildSearchPredicate(SearchGameDto searchGameDto)
        {
            var predicate = PredicateBuilder.New<GameEntity>(g => !g.IsIgnored);
            predicate = predicate.And(g => g.Name.ToLower().Contains(searchGameDto.Name.ToLower()));
            predicate = predicate.And(g => !searchGameDto.Platform.HasValue || g.Platform == searchGameDto.Platform.Value.ToEntity());

            if (searchGameDto.CategoriesId is not null)
            {
                foreach (var categoryId in searchGameDto.CategoriesId)
                {
                    predicate = predicate.And(g => g.Categories!.Select(c => c.Id).Contains(categoryId));
                }
            }

            return predicate;
        }

        public async Task UpdateGame(Guid gameId, GameDto gameDto)
        {
            var game = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories), noTracking: false);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            gameDto.ToEntity(game);

            var actualCategories = gameDto.Categories?.Select(c => c.ToEntity()).ToList() ?? new List<CategoryEntity>();
            game.Categories!.RemoveAll(c => !actualCategories.Any(ac => ac.Id == c.Id));
            game.Categories!.AddRange(actualCategories.Where(ac => !game.Categories!.Any(c => c.Id == ac.Id)));

            await gameRepository.SaveChanges();
        }
    }
}
