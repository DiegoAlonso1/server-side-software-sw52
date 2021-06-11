using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Friendship
    {
        public int user1Id { get; set; }
        public User user1 { get; set; }
        public int user2Id { get; set; }
        public User user2 { get; set; }
    }
}
