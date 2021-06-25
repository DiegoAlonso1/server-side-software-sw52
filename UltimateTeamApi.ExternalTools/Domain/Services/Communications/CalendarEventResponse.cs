using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class CalendarEventResponse : BaseResponse<CalendarEventResource>
    {
        public CalendarEventResponse() : base()
        {
        }

        public CalendarEventResponse(CalendarEventResource resource) : base(resource)
        {
        }

        public CalendarEventResponse(string message) : base(message)
        {
        }
    }
}
