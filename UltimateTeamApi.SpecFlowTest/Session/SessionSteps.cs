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

namespace UltimateTeamApi.SpecFlowTest.Session
{
    [Binding]
    public class SessionSteps : BaseTest
    {
        private string SessionEndpoint { get; set; }

        public SessionSteps()
        {
            SessionEndpoint = $"{ApiUri}api/sessions";
        }



        /**************************************************/
          /*INITIALIZING TEST WITH SOME PERSONS INSTANCES*/
        /**************************************************/

        [When(@"sessions required attributes provided to initialize instances")]
        public void WhenSessionsRequiredAttributesProvidedToInitializeInstances(Table dtos)
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



        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/
        
        [When(@"the person complete the form with the required fields and click the Create Session button")]
        public void WhenThePersonCompleteTheFormWithTheRequiredFieldsAndClickTheCreateSessionButton(Table dto)
        {
            try
            {
                var session = dto.CreateInstance<Domain.Models.Session>();
                var data = JsonData(session);
                var result = Task.Run(async () => await Client.PostAsync(SessionEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Session Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/
        
        [When(@"the person complete the form with required fields of the session with id (.*) and click the Update Session button")]
        public void WhenThePersonCompleteTheFormWithRequiredFieldsOfTheSessionWithIdAndClickTheUpdateSessionButton(int sessionId, Table dto)
        {
            try
            {
                var session = dto.CreateInstance<Domain.Models.Session>();
                var data = JsonData(session);
                var result = Task.Run(async () => await Client.PutAsync($"{SessionEndpoint}/{sessionId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update Session Integration Test Completed");
                var sessionToCompare = ObjectData<Domain.Models.Session>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(sessionToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 3*/
        /**************************************************/
        
        [When(@"the administrator goes to Sessions Page, session list should return")]
        public void WhenTheAdministratorGoesToSessionsPageSessionListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(SessionEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Sessions Integration Test Completed");
            var sessions = ObjectData<List<Domain.Models.Session>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == sessions.Count, "Input and Out sessions count matched");
        }



        /**************************************************/
                            /*SCENARY 4*/
        /**************************************************/
        
        [When(@"the person enters to the session with id (.*) Page")]
        public void WhenThePersonEntersToTheSessionWithIdPage(int sessionId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionEndpoint}/{sessionId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Sessin by Id Integration Test Completed");
        }

        [Then(@"session details should be")]
        public void ThenSessionDetailsShouldBe(Table dto)
        {
            var session = dto.CreateInstance<Domain.Models.Session>();
            var result = Task.Run(async () => await Client.GetAsync($"{SessionEndpoint}/{session.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Session Details Integration Test Completed");
            var sessionToCompare = ObjectData<Domain.Models.Session>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(sessionToCompare));
        }



        /**************************************************/
                            /*SCENARY 5*/
        /**************************************************/

        [When(@"the administrator goes to Sessions Page, and filter by ""(.*)"" name")]
        public void WhenTheAdministratorGoesToSessionsPageAndFilterByName(string sessionName)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionEndpoint}/name/{sessionName}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Sessions by Name Integration Test Completed");
        }
        
        [Then(@"the receiving session with name ""(.*)"" details should be")]
        public void ThenTheReceivingSessionWithNameDetailsShouldBe(string sessionName, Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionEndpoint}/name/{sessionName}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Sessions By Name Details Integration Test Completed");
            var sessionResponse = ObjectData<List<Domain.Models.Session>>(result.Content.ReadAsStringAsync().Result);
            var sessionInput = dto.CreateInstance<Domain.Models.Session>();
            Assert.IsTrue(sessionInput.Id == sessionResponse[0].Id &&
                sessionInput.Name == sessionResponse[0].Name &&
                sessionInput.SessionTypeId == sessionResponse[0].SessionTypeId);
        }

    }
}