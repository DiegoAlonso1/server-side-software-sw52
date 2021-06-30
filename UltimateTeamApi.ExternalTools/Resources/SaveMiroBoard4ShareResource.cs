using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class SaveMiroBoard4ShareResource
    {
        public List<string> Emails { get; set; }
        public string TeamInvitationStrategy { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
