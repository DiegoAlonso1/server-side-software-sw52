using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Calendar.v3;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }



        /******************************************/
                 /*GET ALL CALENDAR EVENTS*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Calendar Events",
            Description = "Get All Calendar Events",
            OperationId = "GetAllCalendarEvents")]
        [SwaggerResponse(200, "List of Calendar Events", typeof(IEnumerable<string>))]

        [HttpGet("calendars/{calendarId}/events")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IEnumerable<string>> GetAllDriveFilesAsync([FromServices] IGoogleAuthProvider auth, string calendarId)
        {
            var result = await _calendarService.GetAllEvents(auth, calendarId);

            return result;
        }

    }
}
