using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class MiroBoardResponse : BaseResponse<MiroBoardResource>
    {
        public MiroBoardResponse(MiroBoardResource resource) : base(resource)
        {
        }

        public MiroBoardResponse(string message) : base(message)
        {
        }
    }
}
