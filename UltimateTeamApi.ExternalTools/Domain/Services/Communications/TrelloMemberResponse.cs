using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class TrelloMemberResponse : BaseResponse<TrelloMemberResource>
    {
        public TrelloMemberResponse(TrelloMemberResource resource) : base(resource)
        {
        }

        public TrelloMemberResponse(string message) : base(message)
        {
        }
    }
}
