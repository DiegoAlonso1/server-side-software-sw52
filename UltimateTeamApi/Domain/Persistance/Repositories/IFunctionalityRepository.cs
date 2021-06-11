using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IFunctionalityRepository
    {
        Task<IEnumerable<Functionality>> ListAsync();
        Task<Functionality> FindByIdAsync(int functionalityId);
    }
}
