using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
    public class NotificationResource
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RemitendId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
