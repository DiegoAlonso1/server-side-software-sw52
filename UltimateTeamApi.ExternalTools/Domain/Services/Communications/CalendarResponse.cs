using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class CalendarResponse : BaseResponse<CalendarResource>
    {
        public CalendarResponse() : base()
        {
        }

        public CalendarResponse(CalendarResource resource) : base(resource)
        {
        }

        public CalendarResponse(string message) : base(message)
        {
        }

    }
}
