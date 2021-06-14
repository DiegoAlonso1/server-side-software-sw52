using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionTypeRepository
    {
        Task<IEnumerable<SessionType>> ListAsync();
        Task<SessionType> FindByIdAsync(int sessionTypeId);
    }
}
