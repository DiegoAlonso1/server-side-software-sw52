using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Converters;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class TrelloService : ITrelloService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private string _token;

        public TrelloService()
        {
            _apiKey = "0389b1cfea7b9c070a520f3dfe6f79db";
            _token = "f1b4780a0b828067813440b902055007abbeea49167a678fa41be286af5834f5";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.trello.com/1/")
            };
        }

        public TrelloAuthenticationResponse AssignToken(string accessToken)
        {
            var response = new TrelloAuthenticationResponse
            {
                AccessToken = accessToken
            };
            return response;
        }

        public async Task<TrelloBoardResponse> DeleteBoardAsync(string boardId, string accessToken)
        {           
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.DeleteAsync($"boards/{boardId}?key={_apiKey}&token={_token}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloBoardResource
                {
                    Id = boardResponse.id,
                    Name = boardResponse.name,
                    OrganizationId = boardResponse.idOrganization,
                    Url = boardResponse.url,
                    ShortUrl = boardResponse.shortUrl,
                };
                return new TrelloBoardResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloBoardResponse($"An error ocurred while deleting a Board: {ex.Message}");
            }
        }

        public async Task<TrelloCardResponse> DeleteCardAsync(string cardId, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.DeleteAsync($"cards/{cardId}?key={_apiKey}&token={_token}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic cardResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloCardResource
                {
                    Id = cardResponse.id,
                    //DateLastActivity = cardResponse.dateLastActivity,
                    BoardId = cardResponse.idBoard,
                    ListId = cardResponse.idList,
                    //ShortId = cardResponse.idShort,
                    Name = cardResponse.name,
                    //Pos = cardResponse.pos,
                    ShortLink = cardResponse.shortLink,
                    //MembersIds = JArrayConverter.JArrayToObjectList(cardResponse.idMembers),
                    ShortUrl = cardResponse.shortUrl,
                    Url = cardResponse.url,
                };
                return new TrelloCardResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloCardResponse($"An error ocurred while deleting a Card: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"members/{memberId}/boards?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();
                
            dynamic listBoardResponse = JsonConvert.DeserializeObject<Object>(model);
               
            var resources = new List<TrelloBoardResource>();
                
            foreach (var board in listBoardResponse)
            {
                resources.Add(new TrelloBoardResource
                {
                    Id = board.id,
                    Name = board.name,
                    OrganizationId = board.idOrganization,
                    DateLastActivity = board.dateLastActivity,
                    MemberCreatorId = board.idMemberCreator,
                    Url = board.url,
                    ShortUrl = board.shortUrl,
                    DateLastView = board.dateLastView,                    
                });
            }

            return resources;
        }

        public async Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"boards/{boardId}/cards?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listCardResponse = JsonConvert.DeserializeObject<Object>(model);

            var resources = new List<TrelloCardResource>();

            foreach (var card in listCardResponse)
            {
                resources.Add(new TrelloCardResource
                {
                    Id = card.id,
                    DateLastActivity = card.dateLastActivity,
                    BoardId = card.idBoard,
                    ListId = card.idList,
                    ShortId = card.idShort,
                    Name = card.name,
                    Pos = card.pos,
                    ShortLink = card.shortLink,
                    MembersIds = JArrayConverter.JArrayToObjectList(card.idMembers),
                    ShortUrl = card.shortUrl,
                    Url = card.url,            
                });
            }

            return resources;
        }

        public async Task<IEnumerable<TrelloListResource>> GetAllListsByBoardIdAsync(string boardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"boards/{boardId}/lists?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listListResponse = JsonConvert.DeserializeObject<Object>(model);

            var resources = new List<TrelloListResource>();

            foreach (var list in listListResponse)
            {
                resources.Add(new TrelloListResource
                {
                    Id = list.id,
                    Name = list.name,
                    BoardId = list.idBoard,
                    Pos = list.pos,
                });
            }

            return resources;
        }

        public async Task<TrelloBoardResponse> GetBoardByIdAsync(string boardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"boards/{boardId}?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);
                
            var resource = new TrelloBoardResource
            {
                Id = boardResponse.id,
                Name = boardResponse.name,
                OrganizationId = boardResponse.idOrganization,
                //DateLastActivity = (string)boardResponse.dateLastActivity,
                //MemberCreatorId = boardResponse.idMemberCreator,
                Url = boardResponse.url,                   
                ShortUrl = boardResponse.shortUrl,
                //DateLastView = (string)boardResponse.dateLastView,
                //Memberships = boardResponse.memberships,
            };

            return new TrelloBoardResponse(resource);          
        }

        public async Task<TrelloCardResponse> GetCardByIdAsync(string cardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"cards/{cardId}?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic cardResponse = JsonConvert.DeserializeObject<Object>(model);

            var resource = new TrelloCardResource
            {
                Id = cardResponse.id,
                DateLastActivity = cardResponse.dateLastActivity,
                BoardId = cardResponse.idBoard,
                ListId = cardResponse.idList,
                ShortId = cardResponse.idShort,
                Name = cardResponse.name,
                Pos = cardResponse.pos,
                ShortLink = cardResponse.shortLink,
                MembersIds = JArrayConverter.JArrayToObjectList(cardResponse.idMembers),
                ShortUrl = cardResponse.shortUrl,
                Url = cardResponse.url,
            };

            return new TrelloCardResponse(resource);
        }

        public async Task<TrelloListResponse> GetListByCardIdAsync(string cardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"cards/{cardId}/list?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listResponse = JsonConvert.DeserializeObject<Object>(model);

            var resource = new TrelloListResource
            {
                Id = listResponse.id,
                Name = listResponse.name,
                BoardId = listResponse.idBoard,
                Pos = listResponse.pos,
            };

            return new TrelloListResponse(resource);
        }

        public async Task<TrelloListResponse> GetListByIdAsync(string listId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"lists/{listId}?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listResponse = JsonConvert.DeserializeObject<Object>(model);

            var resource = new TrelloListResource
            {
                Id = listResponse.id,
                Name = listResponse.name,
                BoardId = listResponse.idBoard,
                Pos = listResponse.pos,
            };

            return new TrelloListResponse(resource);
        }

        public async Task<TrelloMemberResponse> GetMemberByIdAsync(string memberId, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.GetAsync($"members/{memberId}/?key={_apiKey}&token={_token}");
                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic memberResponse = JsonConvert.DeserializeObject<Object>(model);
                var resource = new TrelloMemberResource
                {
                    Id = memberResponse.id,
                    Username = memberResponse.username,
                    FullName = memberResponse.fullName,
                    Email = memberResponse.email,
                    MemberType = memberResponse.memberType,
                    ProfileUrl = memberResponse.url,
                    Status = memberResponse.status,
                    BoardsIds = JArrayConverter.JArrayToStringList(memberResponse.idBoards),
                    OrganizationsIds = JArrayConverter.JArrayToStringList(memberResponse.idOrganizations)
                };

                return new TrelloMemberResponse(resource);
            }
            catch (Exception ex)
            {
                return new TrelloMemberResponse($"An error ocurred while obtaining TrelloMember: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TrelloMemberResource>> GetAllMembersByCardIdAsync(string cardId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"cards/{cardId}/members?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listMembersResponse = JsonConvert.DeserializeObject<Object>(model);

            var resources = new List<TrelloMemberResource>();

            foreach (var member in listMembersResponse)
            {
                resources.Add(new TrelloMemberResource
                {
                    Id = member.id,
                    Username = member.username,
                    FullName = member.fullName,
                    Email = member.email,
                });
            }

            return resources;
        }

        public async Task<TrelloBoardResponse> SaveBoardAsync(SaveTrelloBoardResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.PostAsync($"boards/?key={_apiKey}&token={_token}&name={_resource.Name}", null);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloBoardResource
                {
                    Id = boardResponse.id,
                    Name = boardResponse.name,
                    OrganizationId = boardResponse.idOrganization,                   
                    Url = boardResponse.url,
                    ShortUrl = boardResponse.shortUrl,              
                };
                return new TrelloBoardResponse(resource);

            }
            catch(Exception ex)
            {
                return new TrelloBoardResponse($"An error ocurred while saving a Board: {ex.Message}");
            }
        }

        public async Task<TrelloListResponse> SaveListOnABoardAsync(SaveTrelloListResource _resource, string boardId, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.PostAsync($"boards/{boardId}/lists?key={_apiKey}&token={_token}&name={_resource.Name}", null);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic listResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloListResource
                {
                    Id = listResponse.id,
                    Name = listResponse.name,
                    BoardId = listResponse.idBoard,
                    Pos = listResponse.pos,
                };
                return new TrelloListResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloListResponse($"An error ocurred while saving a List on a Board: {ex.Message}");
            }
        }

        //public async Task<TrelloCardResponse> SaveCardAsync(SaveTrelloCardResource _resource, string listId)
        //{
        //    try
        //    {
        //        var data = _resource.Name;
        //        var request = await _httpClient.PostAsync($"cards?key={_apiKey}&token={_token}&idList={listId}", null);

        //        if (!request.IsSuccessStatusCode)
        //            throw new Exception();

        //        var model = await request.Content.ReadAsStringAsync();

        //        dynamic cardResponse = JsonConvert.DeserializeObject<Object>(model);

        //        var resource = new TrelloCardResource
        //        {
        //            Id = cardResponse.id,
        //            DateLastActivity = cardResponse.dateLastActivity,
        //            BoardId = cardResponse.idBoard,
        //            ListId = cardResponse.idList,
        //            ShortId = cardResponse.idShort,
        //            Name = cardResponse.name,
        //            Pos = cardResponse.pos,
        //            ShortLink = cardResponse.shortLink,
        //            MembersIds = JArrayConverter.JArrayToObjectList(cardResponse.idMembers),
        //            ShortUrl = cardResponse.shortUrl,
        //            Url = cardResponse.url,
        //        };
        //        return new TrelloCardResponse(resource);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new TrelloCardResponse($"An error ocurred while saving a Card: {ex.Message}");
        //    }
        //}

        public async Task<TrelloBoardResponse> UpdateBoardAsync(string boardId, SaveTrelloBoardResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var data = new StringContent(JsonConvert.SerializeObject(new { name = _resource.Name }), Encoding.UTF8, "application/json");
                var request = await _httpClient.PutAsync($"boards/{boardId}?key={_apiKey}&token={_token}", data);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic boardResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloBoardResource
                {
                    Id = boardResponse.id,
                    Name = boardResponse.name,
                    OrganizationId = boardResponse.idOrganization,
                    Url = boardResponse.url,
                    ShortUrl = boardResponse.shortUrl,
                };
                return new TrelloBoardResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloBoardResponse($"An error ocurred while updating a Board: {ex.Message}");
            }
        }

        public async Task<TrelloCardResponse> UpdateCardAsync(string cardId, SaveTrelloCardResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var data = new StringContent(JsonConvert.SerializeObject(new { name = _resource.Name }), Encoding.UTF8, "application/json");
                var request = await _httpClient.PutAsync($"cards/{cardId}?key={_apiKey}&token={_token}", data);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic cardResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloCardResource
                {
                    Id = cardResponse.id,
                    DateLastActivity = cardResponse.dateLastActivity,
                    BoardId = cardResponse.idBoard,
                    ListId = cardResponse.idList,
                    ShortId = cardResponse.idShort,
                    Name = cardResponse.name,
                    Pos = cardResponse.pos,
                    ShortLink = cardResponse.shortLink,
                    MembersIds = JArrayConverter.JArrayToObjectList(cardResponse.idMembers),
                    ShortUrl = cardResponse.shortUrl,
                    Url = cardResponse.url,
                };
                return new TrelloCardResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloCardResponse($"An error ocurred while updating a Card: {ex.Message}");
            }
        }

        public async Task<TrelloListResponse> UpdateListOnABoardAsync(string listId, SaveTrelloListResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var data = new StringContent(JsonConvert.SerializeObject(new { name = _resource.Name }), Encoding.UTF8, "application/json");
                var request = await _httpClient.PutAsync($"lists/{listId}?key={_apiKey}&token={_token}", data);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic listResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloListResource
                {
                    Id = listResponse.id,
                    Name = listResponse.name,
                    BoardId = listResponse.idBoard,
                    Pos = listResponse.pos,
                };
                return new TrelloListResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloListResponse($"An error ocurred while updating a List: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TrelloOrganizationResource>> GetAllOrganizationsByMemberIdAsync(string memberId, string accessToken)
        {
            if (accessToken != null && accessToken.Length > 0)
                _token = accessToken;
            var request = await _httpClient.GetAsync($"members/{memberId}/organizations?key={_apiKey}&token={_token}");

            if (!request.IsSuccessStatusCode)
                throw new Exception();

            var model = await request.Content.ReadAsStringAsync();

            dynamic listOrganizationResponse = JsonConvert.DeserializeObject<Object>(model);

            var resources = new List<TrelloOrganizationResource>();

            foreach (var organization in listOrganizationResponse)
            {
                resources.Add(new TrelloOrganizationResource
                {
                    Id = organization.id,
                    Name = organization.name,
                    DisplayName = organization.displayName,
                    TeamType = organization.teamType,
                    MemberCreatorId = organization.memberCreatorId,
                    Url = organization.url,
                    IxUpdate = organization.ixUpdate,
                    BillableMemberCount = organization.billableMemberCount,
                    ActiveBillableMemberCount = organization.activeBillableMemberCount,
                    BoardsIds= JArrayConverter.JArrayToStringList(organization.idBoards),
                });
            }

            return resources;
        }

        public async Task<TrelloOrganizationResponse> SaveOrganizationAsync(SaveTrelloOrganizationResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.PostAsync($"organizations?key={_apiKey}&token={_token}&displayName={_resource.DisplayName}", null);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic organizationResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloOrganizationResource
                {
                    Id = organizationResponse.id,
                    Name = organizationResponse.name,
                    DisplayName = organizationResponse.displayName,
                    TeamType = organizationResponse.teamType,
                    MemberCreatorId = organizationResponse.memberCreatorId,
                    Url = organizationResponse.url,
                    IxUpdate = organizationResponse.ixUpdate,
                    //BillableMemberCount = organizationResponse.billableMemberCount,
                    //ActiveBillableMemberCount = organizationResponse.activeBillableMemberCount,
                    //BoardsIds = JArrayConverter.JArrayToStringList(organizationResponse.idBoards),
                };
                return new TrelloOrganizationResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloOrganizationResponse($"An error ocurred while saving an Organization: {ex.Message}");
            }
        }

        public async Task<TrelloOrganizationResponse> UpdateOrganizationAsync(string organizationId, SaveTrelloOrganizationResource _resource, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var data = new StringContent(JsonConvert.SerializeObject(new { displayName = _resource.DisplayName }), Encoding.UTF8, "application/json");
                var request = await _httpClient.PutAsync($"organizations/{organizationId}?key={_apiKey}&token={_token}", data);

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic organizationResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloOrganizationResource
                {
                    Id = organizationResponse.id,
                    Name = organizationResponse.name,
                    DisplayName = organizationResponse.displayName,
                    TeamType = organizationResponse.teamType,
                    MemberCreatorId = organizationResponse.memberCreatorId,
                    Url = organizationResponse.url,
                    IxUpdate = organizationResponse.ixUpdate,
                    //BillableMemberCount = organizationResponse.billableMemberCount,
                    //ActiveBillableMemberCount = organizationResponse.activeBillableMemberCount,
                    //BoardsIds = JArrayConverter.JArrayToStringList(organizationResponse.idBoards),
                };
                return new TrelloOrganizationResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloOrganizationResponse($"An error ocurred while updating an Organization: {ex.Message}");
            }
        }

        public async Task<TrelloOrganizationResponse> DeleteOrganizationAsync(string organizationId, string accessToken)
        {
            try
            {
                if (accessToken != null && accessToken.Length > 0)
                    _token = accessToken;
                var request = await _httpClient.DeleteAsync($"organizations/{organizationId}?key={_apiKey}&token={_token}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                dynamic organizationResponse = JsonConvert.DeserializeObject<Object>(model);

                var resource = new TrelloOrganizationResource
                {
                    Id = organizationResponse.id,
                    Name = organizationResponse.name,
                    DisplayName = organizationResponse.displayName,
                    TeamType = organizationResponse.teamType,
                    MemberCreatorId = organizationResponse.memberCreatorId,
                    Url = organizationResponse.url,
                    IxUpdate = organizationResponse.ixUpdate,
                };
                return new TrelloOrganizationResponse(resource);

            }
            catch (Exception ex)
            {
                return new TrelloOrganizationResponse($"An error ocurred while deleting an Organization: {ex.Message}");
            }
        }
    }
}
