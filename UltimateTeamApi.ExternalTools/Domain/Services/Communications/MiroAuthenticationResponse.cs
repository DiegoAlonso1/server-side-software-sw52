using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class MiroAuthenticationResponse
    {
        public string UserId { get; set; }
        public string TeamId{ get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }

    }
}
