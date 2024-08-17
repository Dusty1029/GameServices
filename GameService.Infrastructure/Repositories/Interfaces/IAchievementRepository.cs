﻿using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities;

namespace GameService.Infrastructure.Repositories.Interfaces
{
    public interface IAchievementRepository : IGenericRepository<GameContext, AchievementEntity>
    {
    }
}
