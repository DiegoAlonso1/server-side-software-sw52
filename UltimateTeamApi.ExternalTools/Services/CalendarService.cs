using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class CalendarService : ICalendarService
    {
        public async Task<IEnumerable<string>> GetAllEvents(IGoogleAuthProvider auth, string calendarId)
        {
            Google.Apis.Calendar.v3.CalendarService calendarService = await GetService(auth);

            if (calendarService == null) 
                return new List<string>();

            var result = await calendarService.Events.List(calendarId).ExecuteAsync();
            return result.Items.Select(i => i.Summary).ToList();
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
        

    }
}
