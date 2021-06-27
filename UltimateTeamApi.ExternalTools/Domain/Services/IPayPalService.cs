using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IPayPalService
    {
        //Task<PayPalTokenResponse> GetToken();
        Task<HttpResponseMessage> SuscribeToAPlan(SaveSuscription resource);
    }
}
