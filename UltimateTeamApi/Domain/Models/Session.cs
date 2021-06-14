using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int SessionTypeId { get; set; }
        public SessionType SessionType { get; set; }
        public List<SessionStadistic> SessionStadistics { get;  set; }
        public List<SessionParticipant> SessionParticipants { get; set; }
    }
}
