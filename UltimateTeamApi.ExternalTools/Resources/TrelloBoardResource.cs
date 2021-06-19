using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloBoardResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdOrganization { get; set; }
        public DateTime? DateLastActivity { get; set; }
        public string IdMemberCreator { get; set; }
        public string Url { get; set; }
        public DateTime DateLastView { get; set; }
        public string ShortUrl { get; set; }
        public IEnumerable<TrelloMembershipResource> Memberships { get; set; }

    }
}
