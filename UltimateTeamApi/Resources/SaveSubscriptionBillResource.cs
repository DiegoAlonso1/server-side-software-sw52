using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Resources
{
    public class SaveSubscriptionBillResource
    {
        public bool License { get; set; }
        public DateTime ActiveLicense { get; set; }
        public DateTime DeadlineLicense { get; set; }
        public int PeriodMonths { get; set; }
        public bool Paid { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int PersonId { get; set; }
    }
}
