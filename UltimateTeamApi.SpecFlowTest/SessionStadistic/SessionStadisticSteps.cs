using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

/*****************************************************************************/
/*INSTRUCTIONS*/
/*****************************************************************************/

/*1-You must have run the main project at least once without creating any entity. 
    Otherwise, if you have created any entity, you must delete the database, recreate 
    it and rerun the project so that the IDs match the IDs of these tests. */

/*2-First run the test that initializes some entities instances. Then run 1 test at 
    time. In order as the scenarios have been created. Scenario 1 first, then Scenario 
    2, and so on.*/

namespace UltimateTeamApi.SpecFlowTest.SessionStadistic
{
    [Binding]
    public class SessionStadisticSteps : BaseTest
    {
        private string SessionEndpoint { get; set; }

        public SessionStadisticSteps()
        {
            SessionEndpoint = $"{ApiUri}api/sessions";
        }



        /**************************************************/
        /*INITIALIZING TEST WITH SOME ENTITIES INSTANCES*/
        /**************************************************/

        [When(@"session required attributes provided to initialize instance")]
        public void WhenSessionRequiredAttributesProvidedToInitializeInstance(Table dtos)
        {
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var session = row.CreateInstance<Domain.Models.Session>();
                    var data = JsonData(session);
                    var result = Task.Run(async () => await Client.PostAsync(SessionEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Session Integration Test Completed");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }
        }

        [Then(@"assign the session with Id (.*) with the functionality with Id (.*)")]
        public void ThenAssignTheSessionWithIdWithTheFunctionalityWithId(int sessionId, int functionalityId)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{SessionStadisticEndpoint(sessionId)}/{functionalityId}", null)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Assign Session Stadistic Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the administrator goes to Functionality Usage Page on session with id (.*), the session stadistics list should return")]
        public void WhenTheAdministratorGoesToFunctionalityUsagePageOnSessionWithId(int sessionId, Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(SessionStadisticEndpoint(sessionId))).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Session Stadistics by Session Id Integration Test Completed");
            var sessionStadistics = ObjectData<List<Domain.Models.SessionStadistic>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == sessionStadistics.Count, "Input and Out Session Stadistics count matched");
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the user uses the functionality with id (.*) in the session with id (.*), session stadistics details should be")]
        public void WhenTheUserUsesTheFunctionalityWithIdInTheSessionWithIdSessionStadisticsDetailsShouldBe(int functionalityId, int sessionId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{SessionStadisticEndpoint(sessionId)}/{functionalityId}", null)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Assign Session Stadistic Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }


        private string SessionStadisticEndpoint(int sessionId)
        {
            return $"{ApiUri}api/sessions/{sessionId}/stadistics";
        }
    }
}
