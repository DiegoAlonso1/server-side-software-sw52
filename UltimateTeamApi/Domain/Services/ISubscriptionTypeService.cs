using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface ISubscriptionTypeService
    {
        Task<IEnumerable<SubscriptionType>> GetAllAsync();
        Task<SubscriptionTypeResponse> GetByIdAsync(int subscriptionTypeId);
        Task<SubscriptionTypeResponse> SaveAsync(SubscriptionType subscriptionType);
        Task<SubscriptionTypeResponse> UpdateAsync(int subscriptionTypeId, SubscriptionType subscriptionTypeRequest);
        Task<SubscriptionTypeResponse> DeleteAsync(int subscriptionTypeId);
    }
}
