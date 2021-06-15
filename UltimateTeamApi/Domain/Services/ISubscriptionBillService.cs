using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface ISubscriptionBillService
    {
        Task<IEnumerable<SubscriptionBill>> GetAllAsync();
        Task<IEnumerable<SubscriptionBill>> GetAllBySubscriptionTypeIdAsync(int subscriptionTypeId);
        Task<SubscriptionBillResponse> GetByIdAsync(int subscriptionBillId);
        Task<SubscriptionBillResponse> SaveAsync(SubscriptionBill subscriptionBill); 
        Task<SubscriptionBillResponse> UpdateAsync(int subscriptionBillId, SubscriptionBill subscriptionBillRequest);
        Task<SubscriptionBillResponse> DeleteAsync(int subscriptionBillId);
    }
}
