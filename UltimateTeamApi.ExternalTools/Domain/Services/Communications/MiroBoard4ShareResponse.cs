using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class MiroBoard4ShareResponse: BaseResponse<MiroBoard4ShareResource>
    {
        public MiroBoard4ShareResponse(MiroBoard4ShareResource resource) : base(resource)
        {
        }

        public MiroBoard4ShareResponse(string message) : base(message)
        {
        }
    }
}
