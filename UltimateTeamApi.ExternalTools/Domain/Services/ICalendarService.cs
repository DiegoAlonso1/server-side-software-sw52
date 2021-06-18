using Google.Apis.Auth.AspNetCore3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<string>> GetAllEvents(IGoogleAuthProvider auth, string calendarId);
        
    }
}
