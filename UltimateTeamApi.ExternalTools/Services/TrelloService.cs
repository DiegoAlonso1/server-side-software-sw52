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

        //public async Task<TrelloBoardResponse> GetAllBoardsByMemberId(string memberId)
        //{
        //    try
        //    {
        //        //var productos = "[{\"codigo\":\"Servilleta\",\"cantidad\":2},{\"codigo\":\"Papelhig\",\"cantidad\":1}]";

        //        //var listProductos = JsonConvert.DeserializeObject<List<Producto>>(productos);

        //        //foreach (Producto prod in listProductos)
        //        //{
        //        //    Console.WriteLine("Código: " + prod.Codigo + " - Cantidad: " + prod.Cantidad);
        //        //}
        //        var request = await _httpClient.GetAsync($"members/{memberId}/boards?key={_apiKey}&token={_token}");

        //        if (!request.IsSuccessStatusCode)
        //            throw new Exception();

        //        var model = await request.Content.ReadAsStringAsync();
        //        List<TrelloBoard> result;
        //        if (model.StartsWith("[") && model.EndsWith("]"))
        //        {
        //            var listboardResponse = JsonConvert.DeserializeObject<List<TrelloBoard>>(model);
        //            var resource = new List<TrelloBoardResource>();
        //            foreach (TrelloBoard trelloBoard in listboardResponse)
        //            {
                        
        //            }
        //            //var resource = new List<TrelloBoardResource>();
        //            {

        //                for (int i = 0; i < boardResponse.Count; i++)
        //                {
        //                    resource.Id[i] = boardResponse[i].id;
        //                    Name = boardResponse[i].name;
        //                    IdOrganization = boardResponse[i].idOrganization;
        //                    DateLastActivity = boardResponse[i].dateLastActivity;
        //                    IdMemberCreator = boardResponse[i].idMemberCreator;
        //                    Url = boardResponse[i].url;
        //                    DateLastView = boardResponse[i].dateLastView;
        //                    ShortUrl = boardResponse[i].shortUrl;
        //                    //Memberships = boardResponse.memberships
        //                }

        //            };
        //            return new TrelloBoardResponse(resource);
        //        }
        //        else
        //        {
        //            var boardResponse = JsonConvert.DeserializeObject<TrelloBoard>(model);
        //            result = new List<TrelloBoard>();
        //            result.Add(singleResult);
        //            var resource = new TrelloBoardResource
        //            {
        //                Id = boardResponse.id,
        //                Name = boardResponse.name,
        //                IdOrganization = boardResponse.idOrganization,
        //                DateLastActivity = boardResponse.dateLastActivity,
        //                IdMemberCreator = boardResponse.idMemberCreator,
        //                Url = boardResponse.url,
        //                DateLastView = boardResponse.dateLastView,
        //                ShortUrl = boardResponse.shortUrl
        //                //Memberships = boardResponse.memberships
        //            };
        //            return new TrelloBoardResponse(resource);
        //        }
        //        //List<Movimientos> result;
        //        //var contenido = "[{ \"NumeroOrden\":null,\"Operacion\":null}]";

        //        //if (contenido.StartsWith("[") && contenido.EndsWith("]"))
        //        //{
        //        //    result = JsonConvert.DeserializeObject<List<Movimientos>>(contenido);
        //        //}
        //        //else
        //        //{
        //        //    var singleResult = JsonConvert.DeserializeObject<Movimientos>(contenido);
        //        //    result = new List<Movimientos>();
        //        //    result.Add(singleResult);
        //        //}
        //        //var resource = new TrelloBoardResource
        //        //{
        //        //    Id = boardResponse.id,
        //        //    Name = boardResponse.name,
        //        //    IdOrganization = boardResponse.idOrganization,
        //        //    DateLastActivity = boardResponse.dateLastActivity,
        //        //    IdMemberCreator = boardResponse.idMemberCreator,
        //        //    Url = boardResponse.url,
        //        //    DateLastView = boardResponse.dateLastView,
        //        //    ShortUrl = boardResponse.shortUrl
        //        //    //Memberships = boardResponse.memberships
        //        //};

        //        //return new TrelloBoardResponse(resource);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new TrelloBoardResponse($"An error ocurred while obtaining all TrelloBoards: {ex.Message}");
        //    }
        //}

        public async Task<TrelloBoardResponse> GetBoardById(string boardId)
        {
            try
            {
                var request = await _httpClient.GetAsync($"boards/{boardId}?key={_apiKey}&token={_token}");

                if (!request.IsSuccessStatusCode)
                    throw new Exception();

                var model = await request.Content.ReadAsStringAsync();

                var boardResponse = JsonConvert.DeserializeObject<TrelloBoard>(model);
                var resource = new TrelloBoardResource
                {
                    Id = boardResponse.id,
                    Name = boardResponse.name,
                    IdOrganization = boardResponse.idOrganization,                   
                    Url = boardResponse.url,                   
                    ShortUrl = boardResponse.shortUrl                   
                };

                return new TrelloBoardResponse(resource);
            }
            catch (Exception ex)
            {
                return new TrelloBoardResponse($"An error ocurred while obtaining TrelloBoard: {ex.Message}");
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
            catch (Exception ex)
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
