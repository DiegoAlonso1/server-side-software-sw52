using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloMembershipResource
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string MemberType { get; set; }
        public bool Deactivated { get; set; }
    }
}
