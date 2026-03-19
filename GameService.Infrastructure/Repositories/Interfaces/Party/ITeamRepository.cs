using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Repositories.Interfaces.Party
{
    public interface ITeamRepository : IGenericRepository<GameContext, TeamEntity>
    {
    }
}
