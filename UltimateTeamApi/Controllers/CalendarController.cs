using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Calendar.v3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        /*LOGIN CALENDAR ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Login Calendar Account",
            Description = "Login Calendar Account",
            OperationId = "LoginCalendarAccount")]
        [SwaggerResponse(200, "Calendar Account Logged", typeof(string))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> LoginCalendarAccountAsync([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _calendarService.AssignGoogleCredentialAsync(auth);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Logged");
        }



        /******************************************/
        /*LOGOUT CALENDAR ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Logout Calendar Account",
            Description = "Logout Calendar Account",
            OperationId = "LogoutCalendarAccount")]
        [SwaggerResponse(200, "Calendar Account Logged out", typeof(string))]

        [HttpGet("logout")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> LogoutCalendarAccountAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("Logged out");
            }

            return Ok("You are already logged out");
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
            var result = await _calendarService.GetAllCalendarEventsByCalendarId(auth, calendarId);

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



        /******************************************/
        /*CREATE EVENT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Save Calendar Event",
            Description = "Save a Calendar Event",
            OperationId = "SaveCalendarEvent")]
        [SwaggerResponse(200, "Event Created", typeof(CalendarEventResource))]

        [HttpPost("calendars/{calendarId}/events")]
        [ProducesResponseType(typeof(CalendarEventResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> SaveCalendarEventAsync([FromServices]IGoogleAuthProvider auth, string calendarId, [FromBody]SaveCalendarEventResource resource)
        {
            var result = await _calendarService.SaveCalendarEventAsync(auth, calendarId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*DELETE EVENT*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Delete Calendar Event",
            Description = "Delete a Calendar Event",
            OperationId = "DeleteCalendarEvent")]
        [SwaggerResponse(200, "Event Deleted", typeof(CalendarEventResource))]

        [HttpDelete("calendars/{calendarId}/events")]
        [ProducesResponseType(typeof(CalendarEventResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> DeleteCalendarEventAsync([FromServices]IGoogleAuthProvider auth, string calendarId, string eventId)
        {
            var result = await _calendarService.DeleteCalendarEventAsync(auth, calendarId, eventId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*CREATE CALENDAR*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Save Calendar",
            Description = "Save a Calendar",
            OperationId = "SaveCalendar")]
        [SwaggerResponse(200, "Calendar Created", typeof(CalendarResource))]

        [HttpPost("calendars")]
        [ProducesResponseType(typeof(CalendarResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> SaveCalendarAsync([FromServices] IGoogleAuthProvider auth, [FromBody] SaveCalendarResource resource)
        {
            var result = await _calendarService.SaveCalendarAsync(auth, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*DELETE CALENDAR*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Delete Calendar",
            Description = "Delete a Calendar",
            OperationId = "DeleteCalendar")]
        [SwaggerResponse(200, "Calendar Deleted", typeof(CalendarResource))]

        [HttpDelete("calendars")]
        [ProducesResponseType(typeof(CalendarResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(CalendarService.ScopeConstants.Calendar)]
        public async Task<IActionResult> DeleteCalendarAsync([FromServices] IGoogleAuthProvider auth, string calendarId)
        {
            var result = await _calendarService.DeleteCalendarAsync(auth, calendarId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
