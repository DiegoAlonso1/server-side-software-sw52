using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Contexts;
using UltimateTeamApi.Domain.Persistance.Repositories;

namespace UltimateTeamApi.Persistance.Repositories
{
    public class SessionTypeRepository : BaseRepository, ISessionTypeRepository
    {
        public SessionTypeRepository(AppDbContext context) : base(context)
        {
        }

        public Task<SessionType> FindByIdAsync(int sessionTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SessionType>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}