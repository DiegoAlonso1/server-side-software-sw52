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
using static Google.Apis.Calendar.v3.EventsResource;
using static Google.Apis.Calendar.v3.CalendarsResource;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class CalendarService : ICalendarService
    {
        public async Task<CalendarResponse> AssignGoogleCredentialAsync(IGoogleAuthProvider auth)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

            if (calendarService != null)
                return new CalendarResponse();

            else
                return new CalendarResponse("An error ocurred while loggin in");
        }

        public async Task<IEnumerable<CalendarResource>> GetAllCalendars(IGoogleAuthProvider auth)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

            var result = calendarService.CalendarList.List().ExecuteAsync();
            var calendars = result.Result.Items;

            List<CalendarResource> resources = new List<CalendarResource>();
            
            foreach(var calendar in calendars)
            {
                resources.Add(createCalendarResource(calendar));
            }

            return resources;
        }

        public async Task<IEnumerable<CalendarEventResource>> GetAllCalendarEventsByCalendarId(IGoogleAuthProvider auth, string calendarId)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

            var result = await calendarService.Events.List(calendarId).ExecuteAsync();
            var events = result.Items;

            List<CalendarEventResource> resources = new List<CalendarEventResource>();
            
            foreach(var _event in events)
            {
                resources.Add(createCalendarEventResource(_event));
            }
            return resources;
        }

        public async Task<CalendarResponse> GetCalendarById(IGoogleAuthProvider auth, string calendarId)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

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

        public async Task<CalendarResponse> AssignGoogleCredential(IGoogleAuthProvider auth)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

            if (calendarService != null)
                return new CalendarResponse();

            else
                return new CalendarResponse("An error ocurred while loggin in");
        }

        public async Task<CalendarEventResponse> DeleteCalendarEventAsync(IGoogleAuthProvider auth, string calendarId, string eventId)
        {
            try
            {
                Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

                var request = calendarService.Events.Get(calendarId, eventId);
                request.Fields = "id,summary,start,end,description";
                var result = await request.ExecuteAsync();

                Google.Apis.Calendar.v3.EventsResource.DeleteRequest deleteRequest = calendarService.Events.Delete(calendarId, eventId);
                deleteRequest.Fields = "id,summary,start,end,description";

                var deleteTask = deleteRequest.ExecuteAsync();
                await deleteTask.ContinueWith(s => s.Dispose());

                if (!deleteTask.IsCompletedSuccessfully)
                    throw new Exception("Deleting event request error");

                var resource = createCalendarEventResource(result);
                return new CalendarEventResponse(resource);
            }
            catch (Exception ex)
            {
                return new CalendarEventResponse($"An error ocurred while deleting the event: {ex.Message}");
            }
        }

        public async Task<CalendarEventResponse> SaveCalendarEventAsync(IGoogleAuthProvider auth, string calendarId, SaveCalendarEventResource resource)
        {
            try
            {
                Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

                Google.Apis.Calendar.v3.EventsResource.InsertRequest insertRequest = calendarService.Events.Insert(new Google.Apis.Calendar.v3.Data.Event
                {
                    Summary = resource.Title,
                    Start = new Google.Apis.Calendar.v3.Data.EventDateTime { DateTime = Convert.ToDateTime(resource.StartDate) },
                    End = new Google.Apis.Calendar.v3.Data.EventDateTime { DateTime = Convert.ToDateTime(resource.EndDate) },
                    Description = resource.Description
                }, calendarId);

                insertRequest.Fields = "id,summary,start,end,description";
                Google.Apis.Calendar.v3.Data.Event eventResult = await insertRequest.ExecuteAsync();
                var _resource = createCalendarEventResource(eventResult);

                return new CalendarEventResponse(_resource);
         
            }
            catch ( Exception ex)
            {
                return new CalendarEventResponse($"An error ocurred while saving the event: {ex.Message}");
            }
        }

        public async Task<CalendarResponse> SaveCalendarAsync(IGoogleAuthProvider auth, SaveCalendarResource resource)
        {
            try
            {
                Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

                Google.Apis.Calendar.v3.CalendarsResource.InsertRequest insertRequest = calendarService.Calendars.Insert(new Google.Apis.Calendar.v3.Data.Calendar
                {Summary = resource.Title});

                insertRequest.Fields = "id,summary";
                Google.Apis.Calendar.v3.Data.Calendar calendarResult = await insertRequest.ExecuteAsync();
                var _resource = createCalendarResource(calendarResult);

                return new CalendarResponse(_resource);
            }
            catch(Exception ex)
            {
                return new CalendarResponse($"An error ocurred while saving the calendar: {ex.Message}");
            }
        }

        public async Task<CalendarResponse> DeleteCalendarAsync(IGoogleAuthProvider auth, string calendarId)
        {
            try
            {
                Google.Apis.Calendar.v3.CalendarService calendarService = await getServiceAsync(auth);

                var request = calendarService.Calendars.Get(calendarId);
                request.Fields = "id,summary";
                var result = await request.ExecuteAsync();

                Google.Apis.Calendar.v3.CalendarsResource.DeleteRequest deleteRequest = calendarService.Calendars.Delete(calendarId);
                deleteRequest.Fields = "id,summary";

                var deleteTask = deleteRequest.ExecuteAsync();
                await deleteTask.ContinueWith(s => s.Dispose());

                if (!deleteTask.IsCompletedSuccessfully)
                    throw new Exception("Deleting calendar request error");

                var resource = createCalendarResource(result);
                return new CalendarResponse(resource);
            }
            catch (Exception ex)
            {
                return new CalendarResponse($"An error ocurred while deleting the calendar: {ex.Message}");
            }
        }

        private CalendarEventResource createCalendarEventResource(Google.Apis.Calendar.v3.Data.Event _event)
        {
            return new CalendarEventResource
            {
                Id = _event.Id,
                Title = _event.Summary,
                StartDate = _event.Start.DateTime.ToString(),
                EndDate = _event.End.DateTime.ToString(),
                Description = _event.Description,
            };
        }

        private CalendarResource createCalendarResource(Google.Apis.Calendar.v3.Data.CalendarListEntry calendar)
        {
            return new CalendarResource
            {
                Id = calendar.Id,
                Title = calendar.Summary
            };
        }
        private CalendarResource createCalendarResource(Google.Apis.Calendar.v3.Data.Calendar calendar)
        {
            return new CalendarResource
            {
                Id = calendar.Id,
                Title = calendar.Summary
            };
        }

        private async Task<Google.Apis.Calendar.v3.CalendarService> getServiceAsync(IGoogleAuthProvider auth)
        {
            GoogleCredential credential = await auth.GetCredentialAsync();
            Google.Apis.Calendar.v3.CalendarService calendarService = new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
            return calendarService;
        }
    }
}
