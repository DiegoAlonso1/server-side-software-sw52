using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISubscriptionBillRepository
    {
        Task<IEnumerable<SubscriptionBill>> ListAsync();
        Task<SubscriptionBill> FindByIdAsync(int subscriptionBillId);
        Task AddAsync(SubscriptionBill subscriptionBill);
        void Update(SubscriptionBill subscriptionBill);
        void Remove(SubscriptionBill subscriptionBill);
    }
}
