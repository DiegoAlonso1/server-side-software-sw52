using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class PayPalSubscriptionResponse : BaseResponse<PayPalSubscriptionResource>
    {
        public PayPalSubscriptionResponse()
        {
        }

        public PayPalSubscriptionResponse(PayPalSubscriptionResource resource) : base(resource)
        {
        }

        public PayPalSubscriptionResponse(string message) : base(message)
        {
        }
    }
}
