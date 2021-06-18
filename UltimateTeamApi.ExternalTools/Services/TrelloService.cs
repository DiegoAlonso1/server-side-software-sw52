using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
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
            _httpClient = new HttpClient();
        }

        public async Task<Object> GetAllBoards()
        {
            //var request = await _httpClient.GetAsync($"members/me/?key={_apiKey}&token={_token}");
            //var request = await _httpClient.GetAsync($"authorize");
            try
            {
                var request = await _httpClient.GetAsync($"https://api.trello.com/1/members/me/?key={_apiKey}&token={_token}");
                if (!request.IsSuccessStatusCode)
                    throw new Exception();
                var model = await request.Content.ReadAsStringAsync();

                var boards = JsonConvert.DeserializeObject<Object>(model);

                return boards;
            }
            catch(Exception ex)
            {
                throw new Exception($"Trello murio: {ex.Message}");
            }
            //if (!request.IsSuccessStatusCode)
            //    throw new Exception("Trello murio :)");

            //var model = await request.Content.ReadAsStringAsync();

            //var boards = JsonConvert.DeserializeObject<IEnumerable<TrelloBoardResource>>(model);

            //return boards;
        }
    }
}
