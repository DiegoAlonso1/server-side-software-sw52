using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    class TrelloBoardResponse : BaseResponse<TrelloBoardResource>
    {
        public TrelloBoardResponse(TrelloBoardResource resource) : base(resource)
        {
        }

        public TrelloBoardResponse(string message) : base(message)
        {
        }
    }
}
