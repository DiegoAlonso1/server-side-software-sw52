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

/*1-You must have run the main project at least once. 
    If you have created any entity, you must delete the database, recreate 
    it and rerun the project so that the IDs match the IDs of these tests.*/

/*2-Run 1 test at time. In order as the scenarios have been created. 
    Scenario 1 first, then Scenario 2, and so on. */

namespace UltimateTeamApi.SpecFlowTest.SessionType
{
    [Binding]
    public class SessionTypeSteps : BaseTest
    {
        private string SessionTypeEndpoint { get; set; }

        public SessionTypeSteps()
        {
            SessionTypeEndpoint = $"{ApiUri}api/sessiontypes";
        }



        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the person goes to Create Session Page, session types options list should return")]
        public void WhenThePersonGoesToCreateSessionPageSessionTypesOptionsListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(SessionTypeEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Session Types Integration Test Completed");
            var sessionTypes = ObjectData<List<Domain.Models.SessionType>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == sessionTypes.Count, "Input and Out session type count matched");
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/
        
        [When(@"the person select session type with id (.*)")]
        public void WhenThePersonSelectSessionTypeWithId(int sessionTypeId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{SessionTypeEndpoint}/{sessionTypeId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Session Type by Id Integration Test Completed");
        }
        
        [Then(@"session type details should be")]
        public void ThenSessionTypeDetailsShouldBe(Table dto)
        {
            var sessionType = dto.CreateInstance<Domain.Models.SessionType>();
            var result = Task.Run(async () => await Client.GetAsync($"{SessionTypeEndpoint}/{sessionType.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Session Type Details Integration Test Completed");
            var sessionTypeToCompare = ObjectData<Domain.Models.SessionType>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(sessionTypeToCompare));
        }
    }
}
