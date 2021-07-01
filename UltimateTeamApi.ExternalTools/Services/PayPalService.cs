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
                BaseAddress = new Uri("https://api-m.sandbox.paypal.com/v1/oauth2/token")
            };
        }

        public async Task<PayPalTokenResponse> GetToken()
        {

            var url = "https://api-m.sandbox.paypal.com/v1/oauth2/token";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Accept-Language"] = "en_US";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Headers["Authorization"] = "Basic QWQwUnhBOE9HeFhWcmJJRDU5NUFIVC1MM2t6RzFWTFNLVkgyalZDMUNuaHJoc3lfdlZ5Y0JpT09HQnVUNExpRGtiakNxWmE5S2l4U1YxUmQ6RVAtcUVzUjdDaFNqamx3WC1nQ01nZUlaUzRjaGNzREJnUkMxTmt2TUVYNTVMa3VYZGhHZTBSa3hBVHFPWHgzOEFGZXJiSzdqR2RfV0g3WE8=";

            var data = "grant_type=client_credentials";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return new PayPalTokenResponse($"{httpResponse}");
            /*var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v1/oauth2/token");

            request.Headers.TryAddWithoutValidation("Accept", "application/json");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en_US");

            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("Ad0RxA8OGxXVrbID595AHT-L3kzG1VLSKVH2jVC1Cnhrhsy_vVycBiOOGBuT4LiDkbjCqZa9KixSV1Rd:EP-qEsR7ChSjjlwX-gCMgeIZS4chcsDBgRC1NkvMEX55LkuXdhGe0RkxATqOXx38AFerbK7jGd_WH7XO"));
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            try
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);

                var model = await response.Content.ReadAsStringAsync();
                dynamic tokenResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new PayPalTokenResource
                {
                    AccessToken = tokenResponse.access_token,
                    Scope = tokenResponse.scope,
                    TokenType = tokenResponse.token_type,
                    AppId = tokenResponse.app_id,
                    ExpiresIn = tokenResponse.expires_in,
                    Nonce = tokenResponse.nonce                    
                };
                return new PayPalTokenResponse(resource);
            }
            catch (Exception ex)
            {
                return new PayPalTokenResponse($"An error ocurred while getting de Pay Pal Token: {ex.Message}");
            }         */


        }

        public async Task<PayPalSubscriptionResponse> SuscribeToAPlan(SaveSuscriptionResource resource)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true;

            var httpClient = new HttpClient(handler);
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v1/billing/subscriptions");

            request.Headers.TryAddWithoutValidation("Accept", "application/json");
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer" + await GetToken());
            request.Headers.TryAddWithoutValidation("Prefer", "return=representation");

            string dataString = " \n      \"plan_id\": \""+
                $"{resource.PlanId}"
                +"\",\n      \"start_time\": \""+GetDate().ToString()
                +"\",\n      \"application_context\": {\n        \"brand_name\": \"example company\",\n        \"locale\": \"en-US\",\n        \"shipping_preference\": \"SET_PROVIDED_ADDRESS\",\n        \"user_action\": \"SUBSCRIBE_NOW\",\n        \"payment_method\": {\n          \"payer_selected\": \"PAYPAL\",\n          \"payee_preferred\": \"IMMEDIATE_PAYMENT_REQUIRED\"\n        },\n        \"return_url\": \"https://example.com/returnUrl\",\n        \"cancel_url\": \"https://example.com/cancelUrl\"\n      }\n ";

            request.Content = new StringContent(dataString);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            var model = await response.Content.ReadAsStringAsync();
            dynamic subscriptionResponse = JsonConvert.DeserializeObject<Object>(model);


            var item = new PayPalSubscriptionResource
            {
                PlanId = subscriptionResponse.plan_id,
                StartTime = subscriptionResponse.start_time,
                Link = subscriptionResponse.links[0].href,
            };
            return new PayPalSubscriptionResponse(item);
        }

        public static DateTime GetDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate;
        }
        // A21AAJmpduzvk1hXc3JCybR4k7xAA_vBLqM7O48CjUCa__1mofvn20xFLxQrrAxFPdIvcAlF4KC9Q9428OFOhivhdChV5Zfaw
    }
}
