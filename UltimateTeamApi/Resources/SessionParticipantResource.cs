using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
	public class SessionParticipantResource
	{
        public int PersonId { get; set; }
        public int SessionId { get; set; }
        public bool Creator { get; set; }
    }
}