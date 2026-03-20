using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities.Rallye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Repositories.Interfaces.Rallye
{
    public interface ISpecialTimeRepository : IGenericRepository<GameContext, SpecialTimeEntity>
    {
    }
}
