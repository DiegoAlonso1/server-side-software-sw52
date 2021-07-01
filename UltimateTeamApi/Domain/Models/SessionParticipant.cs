using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class SessionParticipant
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public bool Creator { get; set; }
    }
}
