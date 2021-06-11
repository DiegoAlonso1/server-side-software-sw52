using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
    public class SaveNotificationResource
    {
        public int SenderId { get; set; }
        public int RemitendId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
