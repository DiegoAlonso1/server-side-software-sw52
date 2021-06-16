using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Friendship
    {
        public int PrincipalId { get; set; }
        public User Principal { get; set; }         //User who send invitation
        public int FriendId { get; set; }
        public User Friend { get; set; }            //User who accepted the invitation
    }
}
