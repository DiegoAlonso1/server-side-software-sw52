using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Calendar.v3;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("To use the following endpoints you must first use the Login link outside the swagger (in your browser) to give Google permissions. Then you can return to this page and make use of the endpoints.")]
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
        [SwaggerResponse(200, "List of Calendar Events", typeof(IEnumerable<CalendarEventResource>))]

        [HttpGet("calendars/{calendarId}/events")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IEnumerable<CalendarEventResource>> GetAllCalendarEventsAsync([FromServices] IGoogleAuthProvider auth, string calendarId)
        {
            var result = await _calendarService.GetAllEventsByCalendarId(auth, calendarId);

            return result;
        }



        /******************************************/
        /*GET ALL CALENDARS*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Calendars",
            Description = "Get All Calendars",
            OperationId = "GetAllCalendars")]
        [SwaggerResponse(200, "List of Calendars", typeof(IEnumerable<CalendarResource>))]

        [HttpGet("calendars")]
        [ProducesResponseType(typeof(IEnumerable<CalendarResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IEnumerable<CalendarResource>> GetAllCalendarsAsync([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _calendarService.GetAllCalendars(auth);

            return result;
        }



        /******************************************/
        /*GET CALENDAR BY ID*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Calendar By Id",
            Description = "Get a Calendar by Id ",
            OperationId = "GetCalendarById")]
        [SwaggerResponse(200, "Calendar by Id", typeof(CalendarResource))]

        [HttpGet("calendars/{calendarId}")]
        [ProducesResponseType(typeof(CalendarResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> GetCalendarByIdAsync([FromServices] IGoogleAuthProvider auth, string calendarId)
        {
            var result = await _calendarService.GetCalendarById(auth, calendarId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
