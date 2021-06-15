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
    public class SubscriptionBillRepository : BaseRepository, ISubscriptionBillRepository
    {
        public SubscriptionBillRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SubscriptionBill subscriptionBill)
        {
            await _context.SubscriptionBills.AddAsync(subscriptionBill);
        }

        public async Task<SubscriptionBill> FindByIdAsync(int subscriptionBillId)
        {
            return await _context.SubscriptionBills.FindAsync(subscriptionBillId);
        }

        public async Task<IEnumerable<SubscriptionBill>> ListAsync()
        {
            return await _context.SubscriptionBills.ToListAsync();
        }

        public async Task<IEnumerable<SubscriptionBill>> ListBySubscriptionTypeIdAsync(int subscriptionTypeId)
        {
            return await _context.SubscriptionBills.Where(s => s.SubscriptionTypeId == subscriptionTypeId).ToListAsync();
        }

        public void Remove(SubscriptionBill subscriptionBill)
        {
            _context.SubscriptionBills.Remove(subscriptionBill);
        }

        public void Update(SubscriptionBill subscriptionBill)
        {
            _context.SubscriptionBills.Update(subscriptionBill);
        }
    }
}
