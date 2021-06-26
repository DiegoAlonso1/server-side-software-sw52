using Google.Apis.Auth.AspNetCore3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<CalendarEventResource>> GetAllCalendarEventsByCalendarId(IGoogleAuthProvider auth, string calendarId);
        Task<IEnumerable<CalendarResource>> GetAllCalendars(IGoogleAuthProvider auth);
        Task<CalendarResponse> GetCalendarById(IGoogleAuthProvider auth, string calendarId);
        Task<CalendarResponse> AssignGoogleCredential(IGoogleAuthProvider auth);
        Task<CalendarEventResponse> SaveCalendarEventAsync(IGoogleAuthProvider auth, string calendarId, SaveCalendarEventResource resource);
        Task<CalendarEventResponse> DeleteCalendarEventAsync(IGoogleAuthProvider auth, string calendarId, string eventId);
        Task<CalendarEventResponse> UpdateCalendarEventAsync(IGoogleAuthProvider auth, string calendarId, string eventId, SaveCalendarEventResource resource);
        Task<CalendarResponse> AssignGoogleCredentialAsync(IGoogleAuthProvider auth);
        Task<CalendarResponse> SaveCalendarAsync(IGoogleAuthProvider auth, SaveCalendarResource resource);
        Task<CalendarResponse> DeleteCalendarAsync(IGoogleAuthProvider auth, string calendarId);
        Task<CalendarResponse> UpdateCalendarAsync(IGoogleAuthProvider auth, string calendarId, SaveCalendarResource resource);
    }
}
