﻿using Nancy.Json;
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
        private readonly string _token;

        public TrelloService()
        {
            _apiKey = "0389b1cfea7b9c070a520f3dfe6f79db";
            _token = "f1b4780a0b828067813440b902055007abbeea49167a678fa41be286af5834f5";
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

        public async Task<TrelloBoardResponse> DeleteBoardAsync(string boardId)
        {           
            try
            {
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

        public async Task<TrelloCardResponse> DeleteCardAsync(string cardId)
        {
            try
            {
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

        public async Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId)
        {
          
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

        public async Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId)
        {
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

        public async Task<TrelloBoardResponse> GetBoardByIdAsync(string boardId)
        {
            
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

        public async Task<TrelloCardResponse> GetCardByIdAsync(string cardId)
        {
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

        public async Task<TrelloMemberResponse> GetMemberByIdAsync(string memberId)
        {
            try
            {

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

        public async Task<TrelloBoardResponse> SaveBoardAsync(SaveTrelloBoardResource _resource)
        {
            try
            {
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

        //public async Task<TrelloCardResponse> SaveCardAsync(SaveTrelloCardResource _resource)
        //{
        //    try
        //    {
        //        var request = await _httpClient.PostAsync($"cards?key={_apiKey}&token={_token}&idList={_resource.ListId}", null);

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

        public async Task<TrelloBoardResponse> UpdateBoardAsync(string boardId, SaveTrelloBoardResource _resource)
        {
            try
            {
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

        public async Task<TrelloCardResponse> UpdateCardAsync(string cardId, SaveTrelloCardResource _resource)
        {
            try
            {
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
    }
}
