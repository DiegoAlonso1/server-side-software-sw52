using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class MiroUserResponse:BaseResponse<MiroUserResource>
    {
        public MiroUserResponse(MiroUserResource resource) : base(resource)
        {
        }

        public MiroUserResponse(string message) : base(message)
        {
        }
    }
}
