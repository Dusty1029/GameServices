using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities.Party;
using GameService.Infrastructure.Repositories.Interfaces.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Repositories.Implementations.Party
{
    public class TeamRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, TeamEntity>(context, cancellationTokenService), ITeamRepository
    {
    }
}
