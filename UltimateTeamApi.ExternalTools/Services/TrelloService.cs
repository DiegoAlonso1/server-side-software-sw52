using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Models.Trello;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class TrelloService : ITrelloService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _token;

        public TrelloService()
        {
            _apiKey = "e8dfb267131edf831818c377cf6befc1";
            _token = "9b54840b1e46352f69131ef16b3753fa2678f35c513bb2a70cd2092cc60d05c7";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.trello.com/1/")
            };
        }

        public async Task<TrelloAccountResponse> AssignToken()
        {
           try
            {
                var request = await _httpClient.GetAsync($"authorize?expiration=never&callback_method=postMessage&return_url&name=UltimateTeam&scope=read,write,account&response_type=token&key={_apiKey}");
                return new TrelloAccountResponse();

            }
            catch(Exception ex)
            {
                return new TrelloAccountResponse($"An error ocurred while logging in Trello account: {ex.Message}");
            }
        }

        public async Task<TrelloMemberResponse> GetAllBoardsByMemberId(string memberId)
        {
            try
            {
                var request = await _httpClient.GetAsync($"members/{memberId}/boards?key={_apiKey}&token={_token}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                var memberResponse = JsonConvert.DeserializeObject<TrelloMember>(model);
                var resource = new TrelloMemberResource
                {
                    Id = memberResponse.id,
                    Username = memberResponse.username,
                    FullName = memberResponse.fullName,
                    Email = memberResponse.email,
                    MemberType = memberResponse.memberType,
                    ProfileUrl = memberResponse.url,
                    Status = memberResponse.status,
                    BoardsIds = memberResponse.idBoards,
                    OrganizationsIds = memberResponse.idOrganizations
                };

                return new TrelloMemberResponse(resource);
            }
            catch (Exception ex)
            {
                return new TrelloMemberResponse($"An error ocurred while obtaining TrelloMember: {ex.Message}");
            }
        }

        public async Task<TrelloMemberResponse> GetMemberById(string memberId)
        {         
            try
            {
                var request = await _httpClient.GetAsync($"members/{memberId}/?key={_apiKey}&token={_token}");
                
                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                var memberResponse = JsonConvert.DeserializeObject<TrelloMember>(model);
                var resource = new TrelloMemberResource
                {
                    Id = memberResponse.id,
                    Username = memberResponse.username,
                    FullName = memberResponse.fullName,
                    Email = memberResponse.email,
                    MemberType = memberResponse.memberType,
                    ProfileUrl = memberResponse.url,
                    Status = memberResponse.status,
                    BoardsIds = memberResponse.idBoards,
                    OrganizationsIds = memberResponse.idOrganizations
                };

                return new TrelloMemberResponse(resource);
            }
            catch(Exception ex)
            {
                return new TrelloMemberResponse($"An error ocurred while obtaining TrelloMember: {ex.Message}");
            }
        }

        public async Task<TrelloAccountResponse> UnassignToken()
        {
            try
            {
                var request = await _httpClient.GetAsync($"tokens/{_token}/?key={_apiKey}&token={_token}");
                return new TrelloAccountResponse();

            }
            catch (Exception ex)
            {
                return new TrelloAccountResponse($"An error ocurred while logging out in Trello account: {ex.Message}");
            }
        }
    }
}
