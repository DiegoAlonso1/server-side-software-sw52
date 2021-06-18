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
        Task<IEnumerable<CalendarEventResource>> GetAllEventsByCalendarId(IGoogleAuthProvider auth, string calendarId);
        Task<IEnumerable<CalendarResource>> GetAllCalendars(IGoogleAuthProvider auth);
        Task<CalendarResponse> GetCalendarById(IGoogleAuthProvider auth, string calendarId);
        Task<CalendarResponse> AssignGoogleCredential(IGoogleAuthProvider auth);
    }
}
