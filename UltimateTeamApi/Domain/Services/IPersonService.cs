using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<IEnumerable<Person>> GetAllByAdministratorIdAsync(int administratorId);
        Task<PersonResponse> GetByIdAsync(int personId);
        Task<PersonResponse> GetByEmailAsync(string personEmail);
        Task<PersonResponse> SaveAsync(Person person);
        Task<PersonResponse> UpdateAsync(int personId, Person personRequest);
        Task<PersonResponse> DeleteAsync(int personId);
    }
}
