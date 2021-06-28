using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Friendship
    {
        public int PrincipalId { get; set; }
        public Person Principal { get; set; }         //Person who send invitation
        public int FriendId { get; set; }
        public Person Friend { get; set; }            //Person who accepted the invitation
    }
}
