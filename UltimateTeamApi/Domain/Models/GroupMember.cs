using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class GroupMember
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public bool PersonCreator { get; set; }
    }
}
