using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IFunctionalityService
    {
        Task<IEnumerable<Functionality>> GetAllAsync();
        Task<FunctionalityResponse> GetByIdAsync(int functionalityId);
    }
}
