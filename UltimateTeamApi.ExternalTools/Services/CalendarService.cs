using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class CalendarService : ICalendarService
    {
        public async Task<IEnumerable<CalendarResource>> GetAllCalendars(IGoogleAuthProvider auth)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await GetService(auth);

            var result = calendarService.CalendarList.List().ExecuteAsync();
            var calendars = result.Result.Items;

            List<CalendarResource> resources = new List<CalendarResource>();
            
            foreach(var calendar in calendars)
            {
                resources.Add(new CalendarResource
                {
                    Id = calendar.Id,
                    Title = calendar.Summary
                });
            }

            return resources;
        }

        public async Task<IEnumerable<CalendarEventResource>> GetAllEventsByCalendarId(IGoogleAuthProvider auth, string calendarId)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await GetService(auth);

            var result = await calendarService.Events.List(calendarId).ExecuteAsync();
            var events = result.Items;

            List<CalendarEventResource> resources = new List<CalendarEventResource>();
            
            foreach(var _event in events)
            {
                resources.Add(new CalendarEventResource
                {
                    Id = _event.Id,
                    Title = _event.Summary,
                    StartDate = _event.Start.DateTime.ToString(),
                    EndDate = _event.End.DateTime.ToString(),
                    Description = _event.Description,
                }) ;
            }
            return resources;
        }

        public async Task<CalendarResponse> GetCalendarById(IGoogleAuthProvider auth, string calendarId)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await GetService(auth);

            try
            {
                var result = calendarService.CalendarList.Get(calendarId).ExecuteAsync();
                var calendar = result.Result;

                CalendarResource resource = new CalendarResource { Id = calendar.Id, Title = calendar.Summary };

                return new CalendarResponse(resource);

            }
            catch (Exception ex)
            {
                return new CalendarResponse($"An error ocurred while obtaining the calendar: {ex.Message}");
            }
        }

        private async Task<Google.Apis.Calendar.v3.CalendarService> GetService(IGoogleAuthProvider auth)
        {
            GoogleCredential credential = await auth.GetCredentialAsync();
            Google.Apis.Calendar.v3.CalendarService calendarService = new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
            return calendarService;
        }

        public async Task<CalendarResponse> AssignGoogleCredential(IGoogleAuthProvider auth)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await GetService(auth);

            if (calendarService != null)
                return new CalendarResponse();

            else
                return new CalendarResponse("An error ocurred while loggin in");
        }

    }
}
