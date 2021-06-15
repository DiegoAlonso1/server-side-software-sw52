using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SubscriptionTypeResponse : BaseResponse<SubscriptionType>
    {
        public SubscriptionTypeResponse(SubscriptionType resource) : base(resource)
        {
        }

        public SubscriptionTypeResponse(string message) : base(message)
        {
        }
    }
}
