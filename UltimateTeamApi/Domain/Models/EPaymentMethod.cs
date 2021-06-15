using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public enum EPaymentMethod : byte
    {
        Paypal = 1,
        CreditCard = 2,
        DebitCard = 3
    }
}
