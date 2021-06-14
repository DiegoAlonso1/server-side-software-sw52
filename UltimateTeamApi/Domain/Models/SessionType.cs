using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class SessionType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
