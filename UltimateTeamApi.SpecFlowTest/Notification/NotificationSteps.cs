using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.SpecFlowTest.Notification
{
    class NotificationSteps : BaseTest
    {
        private string NotificationEndpoint { get; set; }

        public NotificationSteps()
        {
            NotificationEndpoint = $"{ApiUri}api/notifications";
        }
    }
}
