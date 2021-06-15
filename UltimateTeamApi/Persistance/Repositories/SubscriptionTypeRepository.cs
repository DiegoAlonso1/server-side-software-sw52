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
    public class SubscriptionTypeRepository : BaseRepository, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SubscriptionType subscriptionType)
        {
            await _context.SubscriptionTypes.AddAsync(subscriptionType);
        }

        public async Task<SubscriptionType> FindByIdAsync(int subscriptionTypeId)
        {
            return await _context.SubscriptionTypes.FindAsync(subscriptionTypeId);
        }

        public async Task<IEnumerable<SubscriptionType>> ListAsync()
        {
            return await _context.SubscriptionTypes.ToListAsync();
        }

        public void Remove(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Remove(subscriptionType);
        }

        public void Update(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Update(subscriptionType);
        }
    }
}
