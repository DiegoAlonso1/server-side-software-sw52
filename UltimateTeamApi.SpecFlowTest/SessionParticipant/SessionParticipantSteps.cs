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
    2, and so on. */

namespace UltimateTeamApi.SpecFlowTest.SessionParticipant
{
    [Binding]
    public class SessionParticipantSteps : BaseTest
    {
        public SessionParticipantSteps()
        {
        }



        /**********************************************************************/
        /*INITIALIZING TEST WITH SOME GROUPS, USERS AND GROUPMEMBERS INSTANCES*/
        /**********************************************************************/

        //[When(@"sessions required attributes provided to initialize instances")]
        //This function is on the SessionSteps.cs

        //[When(@"users required attributes provided to initialize instances")]
        //This function is on the UserSteps.cs

        [Then(@"assign the user with id (.*) with the session with id (.*)")]
        public void ThenAssignTheUserWithIdWithTheSessionWithId(int userId, int sessionId, Table dto)
        {
            try
            {
                var sessionParticipant = dto.CreateInstance<Resources.SaveSessionParticipantResource>();
                var data = JsonData(sessionParticipant);
                var result = Task.Run(async () => await Client.PostAsync($"{SessionParticipantEndpoint(userId)}/{sessionId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Session Participant Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the user with id (.*) click the Join Session button")]
        public void WhenTheUserWithIdClickTheJoinSessionButton(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionParticipantEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Session Participants by UserId Integration Test Completed");
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/
        
        [When(@"the user with id (.*) goes to Sessions History section, session participants list should return")]
        public void WhenTheUserWithIdGoesToSessionsHistorySectionSessionParticipantsListShouldReturn(int userId, Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionParticipantEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Session Participants by UserId Integration Test Completed");
            //var sessionParticipantsToCompare = ObjectData<List<Domain.Models.SessionParticipant>>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(sessionParticipantsToCompare));
        }



        /**************************************************/
                            /*SCENARY 3*/
        /**************************************************/
        
        [When(@"the user with id (.*) goes to Sessions History section and filter sessions created, session participants list should return")]
        public void WhenTheUserWithIdGoesToSessionsHistorySectionAndFilterSessionsCreatedSessionParticipantsListShouldReturn(int userId, Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionParticipantEndpoint(userId)}/creator")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Session Participants by User Creator Id Integration Test Completed");
            //var sessionParticipantsToCompare = ObjectData<List<Domain.Models.SessionParticipant>>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(sessionParticipantsToCompare));
        }



        private string SessionParticipantEndpoint(int userId)
        {
            return $"{ApiUri}api/users/{userId}/sessions";
        }
    }
}
