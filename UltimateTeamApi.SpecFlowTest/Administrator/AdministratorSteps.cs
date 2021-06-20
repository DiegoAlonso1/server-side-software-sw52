using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.SpecFlowTest.Administrator
{
    [Binding]
    public class AdministratorSteps : BaseTest
    {
        private string AdministratorEndpoint { get; set; }

        public AdministratorSteps()
        {
            AdministratorEndpoint = $"{ApiUri}api/administrators";
        }

        /**************************************************/
        /*INITIALIZING TEST WITH SOME ADMINISTRATORS INSTANCES*/
        /**************************************************/

        //[When(@"administrators required attributes provided to initialize instances")]
        //public void WhenAdministratorsRequiredAttributesProvidedToInitializeInstances(Table dtos)
        //{
        //    //ScenarioContext.Current.Pending();
        //}


        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the administrator complete the form with the required fields and click the Register button")]
        public void WhenTheAdministratorCompleteTheFormWithTheRequiredFieldsAndClickTheRegisterButton(Table dto)
        {
            try
            {
                var administrator = dto.CreateInstance<Domain.Models.Administrator>();
                var data = JsonData(administrator);
                var result = Task.Run(async () => await Client.PostAsync(AdministratorEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Administrator Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }


        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the administrator with id (.*) complete the form with required fields and click the Update button")]
        public void WhenTheAdministratorWithIdCompleteTheFormWithRequiredFieldsAndClickTheUpdateButton(int adminId, Table dto)
        {
            try
            {
                var administrator = dto.CreateInstance<Domain.Models.Administrator>();
                var data = JsonData(administrator);
                var result = Task.Run(async () => await Client.PutAsync($"{AdministratorEndpoint}/{adminId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update Administrator Integration Test Completed");
                var administratorToCompare = ObjectData<Domain.Models.Administrator>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(administratorToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }


        /**************************************************/
                            /*SCENARY 3*/
        /**************************************************/

        [When(@"the administrator goes to see all the administrators, administrator list should return")]
        public void WhenTheAdministratorGoesToSeeAllTheAdministratorsAdministratorListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(AdministratorEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Administrators Integration Test Completed");
            var administrators = ObjectData<List<Domain.Models.Administrator>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == administrators.Count, "Input and Out administrator count matched");
        }



        /**************************************************/
                            /*SCENARY 4*/
        /**************************************************/

        [When(@"the administrator with id (.*) goes to Profile Page")]
        public void WhenTheAdministratorWithIdGoesToProfilePage(int adminId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{AdministratorEndpoint}/{adminId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Administrator by Id Integration Test Completed");
        }

        [Then(@"administrator details should be")]
        public void ThenAdministratorDetailsShouldBe(Table dto)
        {
            var administrator = dto.CreateInstance<Domain.Models.Administrator>();
            var result = Task.Run(async () => await Client.GetAsync($"{AdministratorEndpoint}/{administrator.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Administrator Details Integration Test Completed");
            var administratorToCompare = ObjectData<Domain.Models.Administrator>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(administratorToCompare));
        }



        /**************************************************/
                            /*SCENARY 5*/
        /**************************************************/

        [When(@"the administrator with id (.*) click the Delete Account button")]
        public void WhenTheAdministratorWithIdClickTheDeleteAccountButton(int adminId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{AdministratorEndpoint}/{adminId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }     
   
        [Then(@"the administrator with id (.*) is removed")]
        public void ThenTheAdministratorWithIdIsRemoved(int adminId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{AdministratorEndpoint}/{adminId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Delete Administrator Integration Test Completed");
                var administratorToCompare = ObjectData<Domain.Models.Administrator>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(administratorToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }
    }
}
