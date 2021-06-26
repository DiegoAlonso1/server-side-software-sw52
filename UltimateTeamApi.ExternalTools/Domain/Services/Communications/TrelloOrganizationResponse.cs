using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class TrelloOrganizationResponse : BaseResponse<TrelloOrganizationResource>
    {
        public TrelloOrganizationResponse(TrelloOrganizationResource resource) : base(resource)
        {
        }

        public TrelloOrganizationResponse(string message) : base(message)
        {
        }
    }
}
