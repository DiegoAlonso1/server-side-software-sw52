using System;
namespace UltimateTeamApi.ExternalTools.Domain.Models.Trello
{
    public class MessagesDismissed
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public DateTime lastDismissed { get; set; }
    }
}
