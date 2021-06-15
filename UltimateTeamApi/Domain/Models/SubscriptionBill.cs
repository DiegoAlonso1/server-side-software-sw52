using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class SubscriptionBill
    {
        public int Id { get; set; }
        public bool License { get; set; }
        public DateTime ActiveLicense { get; set; }
        public DateTime DeadlineLicense { get; set; }
        public int PeriodMonths { get; set; }
        public bool Paid { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }

        public int SubscriptionTypeId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


    }
}
