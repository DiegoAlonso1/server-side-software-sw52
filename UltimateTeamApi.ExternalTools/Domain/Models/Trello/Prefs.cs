using System.Collections.Generic;

namespace UltimateTeamApi.ExternalTools.Domain.Models.Trello
{
    public class Prefs
    {
        public Privacy privacy { get; set; }
        public bool sendSummaries { get; set; }
        public int minutesBetweenSummaries { get; set; }
        public int minutesBeforeDeadlineToNotify { get; set; }
        public bool colorBlind { get; set; }
        public string locale { get; set; }
        
    }
}
