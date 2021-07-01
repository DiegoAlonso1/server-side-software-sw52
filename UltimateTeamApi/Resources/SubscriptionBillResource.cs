using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Resources
{
    public class SubscriptionBillResource
    {
        public int Id { get; set; }
        public bool License { get; set; }
        public DateTime ActiveLicense { get; set; }
        public DateTime DeadlineLicense { get; set; }
        public int PeriodMonths { get; set; }
        public bool Paid { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Person person { get; set; }
    }
}
