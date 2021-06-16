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

namespace UltimateTeamApi.SpecFlowTest.Functionality
{
    [Binding]
    public class FunctionalitySteps : BaseTest
    {
        private string FunctionalityEndpoint { get; set; }

        public FunctionalitySteps()
        {
            FunctionalityEndpoint = $"{ApiUri}api/functionalities";
        }



        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the administrator goes to Functionality Usage Page, functionalities list should return")]
        public void WhenTheAdministratorGoesToFunctionalityUsagePageFunctionalitiesListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(FunctionalityEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Functionalities Integration Test Completed");
            var functionalities = ObjectData<List<Domain.Models.Functionality>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == functionalities.Count, "Input and Out user count matched");
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the administrator select functionality with id (.*)")]
        public void WhenTheAdministratorSelectFunctionalityWithId(int functionalityId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{FunctionalityEndpoint}/{functionalityId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Functionality by Id Integration Test Completed");
        }
        
        [Then(@"functionality details should be")]
        public void ThenFunctionalityDetailsShouldBe(Table dto)
        {
            var functionality = dto.CreateInstance<Domain.Models.Functionality>();
            var result = Task.Run(async () => await Client.GetAsync($"{FunctionalityEndpoint}/{functionality.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Functionality Details Integration Test Completed");
            var functionalityToCompare = ObjectData<Domain.Models.Functionality>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(functionalityToCompare));
        }



        /**************************************************/
                            /*SCENARY 3*/
        /**************************************************/

        [Then(@"functionality stadistics details should be")]
        public void ThenFunctionalityStadisticsDetailsShouldBe(Table dto)
        {
            var sessionStadistic = dto.CreateInstance<Domain.Models.SessionStadistic>();
            var result = Task.Run(async () => await Client.GetAsync($"{FunctionalityEndpoint}/{sessionStadistic.FunctionalityId}/sessions")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Session Stadistics Details Integration Test Completed");
            var sessionStadisticToCompare = ObjectData<Domain.Models.SessionStadistic>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(sessionStadisticToCompare));
        }
    }
}
