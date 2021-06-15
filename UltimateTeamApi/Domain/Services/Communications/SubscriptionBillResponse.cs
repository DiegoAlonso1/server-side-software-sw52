using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SubscriptionBillResponse : BaseResponse<SubscriptionBill>
    {
        public SubscriptionBillResponse(SubscriptionBill resource) : base(resource)
        {
        }

        public SubscriptionBillResponse(string message) : base(message)
        {
        }
    }
}
