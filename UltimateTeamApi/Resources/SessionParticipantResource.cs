using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
	public class SessionParticipantResource
	{
        public int UserId { get; set; }
        public int SessionsId { get; set; }
        public bool Creator { get; set; }
    }
}
