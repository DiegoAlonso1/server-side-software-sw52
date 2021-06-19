using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloMembershipResource
    {
        public string id { get; set; }
        public string idMember { get; set; }
        public string memberType { get; set; }
        public bool deactivated { get; set; }
    }
}
