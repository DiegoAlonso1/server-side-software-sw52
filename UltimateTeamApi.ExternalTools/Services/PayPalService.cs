using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using System.Net.Http;
using System.Net.Http.Headers;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Services
{ 
    public class PayPalService : IPayPalService
    {
        public  async Task<HttpResponseMessage> GetToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v1/oauth2/token");

            request.Headers.TryAddWithoutValidation("Accept", "application/json");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en_US");

            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("Ad0RxA8OGxXVrbID595AHT-L3kzG1VLSKVH2jVC1Cnhrhsy_vVycBiOOGBuT4LiDkbjCqZa9KixSV1Rd:EP-qEsR7ChSjjlwX-gCMgeIZS4chcsDBgRC1NkvMEX55LkuXdhGe0RkxATqOXx38AFerbK7jGd_WH7XO"));
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            
            return response;
        }

        public async Task<HttpResponseMessage> SuscribeToAPlan(SaveSuscription resource)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true;

            var httpClient = new HttpClient(handler);
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v1/billing/subscriptions");

            request.Headers.TryAddWithoutValidation("Accept", "application/json");
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer" + await GetToken());
            request.Headers.TryAddWithoutValidation("Prefer", "return=representation");

            string dataString = " \n      \"plan_id\": \""+
                resource.PlanId
                +"\",\n      \"start_time\": \""+GetDate()
                +"\",\n      \"application_context\": {\n        \"brand_name\": \"example company\",\n        \"locale\": \"en-US\",\n        \"shipping_preference\": \"SET_PROVIDED_ADDRESS\",\n        \"user_action\": \"SUBSCRIBE_NOW\",\n        \"payment_method\": {\n          \"payer_selected\": \"PAYPAL\",\n          \"payee_preferred\": \"IMMEDIATE_PAYMENT_REQUIRED\"\n        },\n        \"return_url\": \"https://example.com/returnUrl\",\n        \"cancel_url\": \"https://example.com/cancelUrl\"\n      }\n ";

            request.Content = new StringContent(dataString);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await httpClient.SendAsync(request);
            return response;
        }

        public DateTime GetDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate;
        }
        // A21AAJmpduzvk1hXc3JCybR4k7xAA_vBLqM7O48CjUCa__1mofvn20xFLxQrrAxFPdIvcAlF4KC9Q9428OFOhivhdChV5Zfaw
    }
}
