using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class PayPalTokenResponse : BaseResponse<PayPalTokenResource>
    {
        public PayPalTokenResponse() : base()
        {
        }

        public PayPalTokenResponse(PayPalTokenResource resource) : base(resource)
        {
        }

        public PayPalTokenResponse(string message) : base(message)
        {
        }

    }
}
