using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities.Rallye;
using GameService.Infrastructure.Repositories.Interfaces.Rallye;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Repositories.Implementations.Rallye
{
    public class RallyeRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, RallyeEntity>(context, cancellationTokenService), IRallyeRepository
    {
    }
}
