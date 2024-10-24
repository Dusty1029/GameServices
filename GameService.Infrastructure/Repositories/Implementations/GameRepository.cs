﻿using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class GameRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, GameEntity>(context, cancellationTokenService), IGameRepository
    {
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

        public async Task<int> NextPlayOrderBySerie(Guid serieId)
        {
            var numberOfGame = await GetCount(g => g.SerieId == serieId);
            if (numberOfGame > 0) 
            {
                return (await DbSet.AsQueryable().AsNoTracking().Where(g => g.SerieId == serieId).MaxAsync(g => g.PlayOrder)) + 1;
            }
            else
            {
                return 0;
            }
            
        }
            
    }
}
