using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class TrelloAccountResponse 
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public TrelloAccountResponse()
        {
            Success = true;
        }

        public TrelloAccountResponse(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
