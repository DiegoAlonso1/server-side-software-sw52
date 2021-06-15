using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class SubscriptionType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Amount { get; set; }
        public List<SubscriptionBill> SubscriptionBills { get; set; }

    }
}
