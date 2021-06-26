using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloMemberResource
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MemberType { get; set; }
        public string ProfileUrl { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> BoardsIds { get; set; }
        public IEnumerable<string> OrganizationsIds { get; set; }

    }
}
