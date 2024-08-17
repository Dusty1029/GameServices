using CommonV2.Models;
using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using GameServices.API.Extensions.Entities;
using GameServices.API.Extensions.Entities.Enums;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameServices.API.BusinessLogics.Implementations
{
    public class GameBL : IGameBL
    {
        private readonly IGameRepository _gameRepository;

        public GameBL(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Guid> CreateGame(GameDto createGameDto)
        {
            var game = await _gameRepository.CreateGame(createGameDto.ToEntity(), createGameDto.Categories?.Select(c => c.ToEntity()).ToList());
            return game.Id;
        }

        public async Task DeleteGameById(Guid gameId)
        {
            var game = await _gameRepository.Find(g => g.Id == gameId);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            await _gameRepository.DeleteAndSave(game);
        }

        public async Task<GameDto> GetGameById(Guid gameId)
        {
            var game = await _gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories).Include(g => g.Achievements));
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            return game.ToDto();
        }

        public async Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto)
        {
            var gamesSearched = await _gameRepository.Search(searchGameDto.Size, searchGameDto.Page, BuildSearchPredicate(searchGameDto));
            
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
            

            foreach(var categoryId in searchGameDto.CategoriesId)
            {
                predicate = predicate.And(g => g.Categories!.Select(c => c.Id).Contains(categoryId));
            }

            return predicate;
        }

        public async Task UpdateGame(Guid gameId, GameDto gameDto)
        {
            var game = await _gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Categories), noTracking: false);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");

            gameDto.ToEntity(game);

            var actualCategories = gameDto.Categories?.Select(c => c.ToEntity()).ToList() ?? new List<CategoryEntity>();
            game.Categories!.RemoveAll(c => !actualCategories.Any(ac => ac.Id == c.Id));
            game.Categories!.AddRange(actualCategories.Where(ac => !game.Categories!.Any(c => c.Id == ac.Id)));

            await _gameRepository.SaveChanges();
        }
    }
}
