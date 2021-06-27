using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloOrganizationResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string TeamType { get; set; }
        public string MemberCreatorId { get; set; }
        public string Url { get; set; }
        public string IxUpdate { get; set; }
        //public List<TrelloMembershipResource> Memberships { get; set; }
        public int BillableMemberCount { get; set; }
        public int ActiveBillableMemberCount { get; set; }
        public IEnumerable<string> BoardsIds { get; set; }
    }
}
