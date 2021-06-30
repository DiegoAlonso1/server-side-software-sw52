using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UltimateTeamApi.SpecFlowTest.Notification
{
    [Binding]
    public class NotificationSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        private string NotificationEndpoint { get; set; }

        public NotificationSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
            NotificationEndpoint = $"{ApiUri}api/notifications";
        }

        /**************************************************/
        /*SCENARY 1*/
        /**************************************************/

        [When(@"the user with Id (.*) sends a notification to the user with Id (.*), the notifications details should be")]
        public void WhenTheUserWithIdSendsANotificationToTheUserWithIdTheNotificationsDetailsShouldBe(int remitendId, int senderId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{UserNotificationEndpoint(senderId)}/{remitendId}", null)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Assign Notification Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
                throw;
            }
        }



        /**************************************************/
        /*SCENARY 2*/
        /**************************************************/

        [When(@"the user with Id (.*) recieves a notification of the user with Id (.*), the notifications details should be")]
        public void WhenTheUserWithIdRecievesANotificationOfTheUserWithIdTheNotificationsDetailsShouldBe(int senderId, int remitendId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.GetAsync($"{UserNotificationEndpoint(senderId)}/{remitendId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Notification Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
                throw;
            }
        }



        /**************************************************/
        /*SCENARY 3*/
        /**************************************************/

        [When(@"the user with Id (.*) wants to see his list of notifications")]
        public void WhenTheUserWithIdWantsToSeeHisListOfNotifications(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{userId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get User by Id Integration Test Completed");
        }
        
        [Then(@"the notification list of the user with Id (.*) should be")]
        public void ThenTheNotificationListOfTheUserWithIdShouldBe(int userId, Table dto)
        {
            var notifications = dto.CreateInstance<List<Domain.Models.Notification>>();
            var result = Task.Run(async () => await Client.GetAsync($"{UserNotificationEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Notifications Details Integration Test Completed");
        }



        private string UserNotificationEndpoint(int userId)
        {
            return $"{ApiUri}api/users/{userId}/notifications";
        }
    }
}
