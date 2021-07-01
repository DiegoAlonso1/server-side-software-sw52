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
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace UltimateTeamApi.ExternalTools.Services
{ 
    public class PayPalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _secret;
        public PayPalService()
        {
            _clientId = "Ad0RxA8OGxXVrbID595AHT-L3kzG1VLSKVH2jVC1Cnhrhsy_vVycBiOOGBuT4LiDkbjCqZa9KixSV1Rd";
            _secret = "EP-qEsR7ChSjjlwX-gCMgeIZS4chcsDBgRC1NkvMEX55LkuXdhGe0RkxATqOXx38AFerbK7jGd_WH7XO";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-m.sandbox.paypal.com/v1/")
            };

        }

        public async Task<PayPalTokenResponse> GetTokenAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en_US");
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", $"{_clientId}:{_secret}");
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
               .GetBytes(_clientId + ":" + _secret));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);


                var data = JsonConvert.SerializeObject("grant_type=client_credentials");
                var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

                var request = await _httpClient.PostAsync("oauth2/token ", content);

                var model = await content.ReadAsStringAsync();

                dynamic tokenResponse = JsonConvert.DeserializeObject<Object>(model);

                if (!request.IsSuccessStatusCode)
                    throw new Exception("PayPal operation result was bad request");


                var _resource = new PayPalTokenResource
                {
                    AccessToken = tokenResponse.access_token,
                    AppId = tokenResponse.app_id,
                    Scope = tokenResponse.scope,
                    TokenType = tokenResponse.token_type,
                    ExpiresIn = tokenResponse.expires_in,
                    Nonce = tokenResponse.nonce
                };

                return new PayPalTokenResponse(_resource);
            }
            catch(Exception ex)
            {
                return new PayPalTokenResponse($"An error ocurred while getting the Token: {ex.Message}");

            }

        }
        public async Task<PayPalSubscriptionResponse> SuscribePlanAsync(string token, SavePaypalSuscriptionResource resource)
        {          
                
            try
            {
                if (token == null || token.Length <= 0)
                    throw new Exception("Token was not assigned");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");


                dynamic resourceObject = new
                {
                    plan_id = resource.PlanId,
                    start_time = resource.StartDate
                };

                var data = JsonConvert.SerializeObject(resourceObject);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var request = await _httpClient.PostAsync("billing/subscriptions", content);

                if (!request.IsSuccessStatusCode)
                    throw new Exception("PayPal operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic subscriptionResponse = JsonConvert.DeserializeObject<Object>(model);

                

                var _resource = new PayPalSubscriptionResource {
                    PlanId = subscriptionResponse.id,
                    StartTime = subscriptionResponse.create_time,
                    Link = subscriptionResponse.links[0].href
                };

                return new PayPalSubscriptionResponse(_resource);
            }
            catch(Exception ex)
            {
                return new PayPalSubscriptionResponse($"An error ocurred while subscribing to a PayPal plan: {ex.Message}");
            }
            
        }

        public static string GetDate()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        // A21AAJmpduzvk1hXc3JCybR4k7xAA_vBLqM7O48CjUCa__1mofvn20xFLxQrrAxFPdIvcAlF4KC9Q9428OFOhivhdChV5Zfaw
    }
}
