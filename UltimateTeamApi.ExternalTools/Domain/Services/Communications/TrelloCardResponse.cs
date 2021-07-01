using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class TrelloCardResponse : BaseResponse<TrelloCardResource>
    {
        public TrelloCardResponse(TrelloCardResource resource) : base(resource)
        {
        }

        public TrelloCardResponse(string message) : base(message)
        {
        }
    }
}
