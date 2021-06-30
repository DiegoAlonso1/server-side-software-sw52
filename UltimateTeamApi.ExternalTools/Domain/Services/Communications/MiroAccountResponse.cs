using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class MiroAccountResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public MiroAccountResponse()
        {
            Success = true;
        }

        public MiroAccountResponse(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
