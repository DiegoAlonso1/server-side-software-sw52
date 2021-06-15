using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IAdministratorService
    {
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task<AdministratorResponse> GetByIdAsync(int administratorId);
        Task<AdministratorResponse> GetByAreaAsync(string area);
        Task<AdministratorResponse> SaveAsync(User administrator);
        Task<AdministratorResponse> UpdateAsync(int administratorId, Administrator administratorRequest);
        Task<AdministratorResponse> DeleteAsync(int administratorId);
    }
}
