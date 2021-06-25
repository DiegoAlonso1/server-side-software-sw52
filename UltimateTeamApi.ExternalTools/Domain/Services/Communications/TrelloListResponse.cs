using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class TrelloListResponse : BaseResponse<TrelloListResource>
    {
        public TrelloListResponse(TrelloListResource resource) : base(resource)
        {
        }

        public TrelloListResponse(string message) : base(message)
        {
        }
    }
}
