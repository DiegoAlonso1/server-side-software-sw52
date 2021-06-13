using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
    public class GroupMemberResource
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool UserCreator { get; set; }
    }
}
