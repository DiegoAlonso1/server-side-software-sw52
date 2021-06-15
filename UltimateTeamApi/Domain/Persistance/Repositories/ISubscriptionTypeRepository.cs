using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISubscriptionTypeRepository
    {
        Task<IEnumerable<SubscriptionType>> ListAsync();
        Task<SubscriptionType> FindByIdAsync(int subscriptionTypeId);
        Task AddAsync(SubscriptionType subscriptionType);
        void Update(SubscriptionType subscriptionType);
        void Remove(SubscriptionType subscriptionType);
    }
}
