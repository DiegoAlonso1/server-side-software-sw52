using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class MiroService : IMiroService
    {
        private HttpClient _httpClient;

        private readonly string _clientSecret;
        private string _accessToken = "";
        private readonly string _redirectUri;
        public MiroService()
        {
            _clientSecret = "0l8a1ooBFmdc0AX1tK6mJnK0Geequd3D";
            _redirectUri = "https://localhost:44345/api/miro/login";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.miro.com/")
            };
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "pIvMbRLpdeeD-di4qC3ATBgxnzk");
            //_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public async Task<MiroAuthenticationResponse> GetAuthAsync(string authCode, string clientId, string teamId)
        {
            //var request = await _httpClient.PostAsync($"oauth/authorize?response_type=code&client_id={_clientId}&redirect_uri=https://localhost:44345/api/miro/login");
            var request = await _httpClient.PostAsync($"v1/oauth/token?grant_type=authorization_code&code={authCode}&redirect_uri={_redirectUri}&client_id={clientId}&client_secret={_clientSecret}", null);
            
            if (!request.IsSuccessStatusCode)
                throw new Exception("Miro operation result was bad request");

            var model = await request.Content.ReadAsStringAsync();

            dynamic authResponse = JsonConvert.DeserializeObject<Object>(model);

            var response = new MiroAuthenticationResponse
            { 
                UserId = authResponse.user_id,
                TeamId = authResponse.team_id,
                Scope = authResponse.scope,
                TokenType = authResponse.token_type,
                AccessToken = authResponse.access_token
            };
            _accessToken = response.AccessToken;
            

            return response;
        }

        //BOARD CONTENT

        public async Task<MiroBoardResponse> CreateMiroBoardAsync(SaveMiroBoardResource resource, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                dynamic resourceObject = new
                {
                    name = resource.Name,
                    sharingPolicy = new
                    {
                        access = resource.SharingPolicyAccess,
                        teamAccess = resource.SharingPolicyTeamAccess
                    },
                    description = resource.Description
                };
                var data = JsonConvert.SerializeObject(resourceObject);
                var new1 = new StringContent(data, Encoding.UTF8, "application/json");

                var request = await _httpClient.PostAsync($"v1/boards", new1);

                if (!request.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResource(boardResponse);
                return new MiroBoardResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroBoardResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }


        public async Task<MiroBoardResponse> GetMiroBoardByIdAsync(string boardId, string accessToken)
        {
            try
            {

                var request = await _httpClient.GetAsync($"v1/boards/{boardId}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResource(boardResponse);
                return new MiroBoardResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroBoardResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }

        public async Task<IEnumerable<MiroBoard4ShareResource>> ShareMiroBoardAsync(string boardId, SaveMiroBoard4ShareResource resource, string accessToken)
        {

            dynamic resourceObject = new
            {
                emails = resource.Emails,
                teamInvitationStrategy = resource.TeamInvitationStrategy,
                role = resource.Role,
                message = resource.Message
            };
            var data = JsonConvert.SerializeObject(resourceObject);
            var new1 = new StringContent(data, Encoding.UTF8, "application/json");

            var request = await _httpClient.PostAsync($"v1/boards/{boardId}/share", new1);

            if (!request.IsSuccessStatusCode)
                return new List<MiroBoard4ShareResource>();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listBoardResponse = JsonConvert.DeserializeObject<Object>(model);

            var _resources = new List<MiroBoard4ShareResource>();

            foreach (var board in listBoardResponse)
            {
                _resources.Add(createResourceMiroBoard4Share(board,accessToken));
            }

            return _resources;
        }

        public async Task<MiroBoardResponse> UpdateMiroBoardAsync(string boardId, SaveMiroBoardResource resource, string accessToken)
        {
            try
            {

                dynamic resourceObject = new
                {
                    name = resource.Name,
                    sharingPolicy = new
                    {
                        access = resource.SharingPolicyAccess,
                        teamAccess = resource.SharingPolicyTeamAccess
                    },
                    description = resource.Description
                };
                var data = JsonConvert.SerializeObject(resourceObject);
                var new1 = new StringContent(data, Encoding.UTF8, "application/json");

                var request = await _httpClient.PatchAsync($"v1/boards/{boardId}", new1);

                if (!request.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResource(boardResponse);
                return new MiroBoardResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroBoardResponse($"An error ocurred while updating a Board: {ex.Message}");
            }
        }

        public async Task<MiroBoardResponse> DeleteMiroBoardbyIdAync(string boardId, string accessToken)
        {
            try
            {

                var requestG = await _httpClient.GetAsync($"v1/boards/{boardId}");
                if (!requestG.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var requestD = await _httpClient.DeleteAsync($"boards/{boardId}");
                if (!requestD.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await requestG.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResource(boardResponse);
                return new MiroBoardResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroBoardResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }

        //USERS

        public async Task<MiroUserResponse> GetMiroUserByIdAsync(string userId, string accessToken)
        {
            try
            {

                var request = await _httpClient.GetAsync($"v1/users/{userId}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResourceMiroUser(boardResponse);
                return new MiroUserResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroUserResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }

        public async Task<MiroUserResponse> GetMyUserAsync(string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var request = await _httpClient.GetAsync($"v1/users/me");

                if (!request.IsSuccessStatusCode)
                    throw new Exception("Miro operation result was bad request");

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var _resource = createResourceMiroUser(boardResponse);
                return new MiroUserResponse(_resource);

            }
            catch (Exception ex)
            {
                return new MiroUserResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }

        private MiroBoardResource createResource(dynamic data)
        {
            return new MiroBoardResource
            {
                Type = data.type,
                Id = data.id,
                Name = data.name,
                ModifiedAt = data.modifiedAt,
                //CreatedBy = data.createdBy,
                //ModifiedBy = data.modifiedBy,
                //Owner = data.owner,
                Description = data.description,
                SharingPolicyAccess = data.sharingPolicy.access,
                SharingPolicyTeamAccess = data.sharingPolicy.teamAccess,
                ViewLink = data.viewLink
            };
        }
        private MiroBoard4ShareResource createResourceMiroBoard4Share(dynamic data, string accessToken)
        {
            string userId = data.user.id;
            return new MiroBoard4ShareResource
            {
                Id = data.id,
                //User = new MiroUserResource
                //{
                //    Type = data.user.type,
                //    Name = data.user.name,
                //    Id = data.user.id,
                //},
                User = GetMiroUserByIdAsync(userId,accessToken).Result.Resource,
                Role = data.role
            };
        }
        private MiroUserResource createResourceMiroUser(dynamic data)
        {
            return new MiroUserResource
            {
                Type = data.type,
                CreatedAt = data.createdAt,
                Id = data.id,
                Name = data.name,
                Role = data.role,
                State = data.state,
                Email = data.email
            };
        }

        //WIDGETS


    }
}
