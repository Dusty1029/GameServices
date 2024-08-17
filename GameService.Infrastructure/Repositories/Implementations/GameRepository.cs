﻿using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class GameRepository : GenericRepository<GameContext, GameEntity>, IGameRepository
    {
        public GameRepository(GameContext context, ICancellationTokenService cancellationTokenService) : base(context, cancellationTokenService)
        {
        }

        public async Task<GameEntity> CreateGame(GameEntity gameEntity, List<CategoryEntity>? categories)
        {
            var transaction = await Context.Database.BeginTransactionAsync();
            await InsertAndSave(gameEntity);
            if (categories is not null)
            {
                gameEntity.Categories = categories;
                await SaveChanges();
            }
            
            await transaction.CommitAsync(CancellationToken);
            return gameEntity;
        }
    }
}
