using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UltimateTeamApi.Domain.Models;

    /*****************************************************************************/
                                /*INSTRUCTIONS*/
    /*****************************************************************************/

    /*1-You must have run the main project at least once without creating any entity. 
        Otherwise, if you have created any entity, you must delete the database, recreate 
        it and rerun the project so that the IDs match the IDs of these tests. */

    /*2-First run the test that initializes some entities instances. Then run 1 test at 
        time. In order as the scenarios have been created. Scenario 1 first, then Scenario 
        2, and so on. */

namespace UltimateTeamApi.SpecFlowTest.Person
{
    [Binding]
    public class PersonSteps : BaseTest
    {
        private string PersonEndpoint { get; set; }
        private string AdministratorEndpoint { get; set; }

        public PersonSteps()
        {
            PersonEndpoint = $"{ApiUri}api/persons";
            AdministratorEndpoint = $"{ApiUri}api/administrators";
        }



        /**************************************************/
          /*INITIALIZING TEST WITH SOME PERSONS INSTANCES*/
        /**************************************************/

        [When(@"persons required attributes provided to initialize instances")]
        public void WhenPersonsRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            //Ensure there is an administrator with id 1
            var jsonAdmin = Task.Run(async () => await Client.GetAsync($"{AdministratorEndpoint}/1")).Result;

            if (jsonAdmin == null || jsonAdmin.StatusCode != HttpStatusCode.OK)
            {
                try
                {
                    var admin = new Domain.Models.Administrator { Id = 1, Name = "Pedro", Password = "Password", Area = "Area151" };
                    var JsonAdmin = JsonData(admin);
                    Task.Run(async () => await Client.PostAsync(AdministratorEndpoint, JsonAdmin));
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }

            //Creating some persons
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var persons = row.CreateInstance<Domain.Models.Person>();
                    var data = JsonData(persons);
                    var result = Task.Run(async () => await Client.PostAsync(PersonEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Person Integration Test Completed");
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

        [When(@"the person complete the form with the required fields and click the Register button")]
        public void WhenThePersonCompleteTheFormWithTheRequiredFieldsAndClickTheRegisterButton(Table dto)
        {
            try
            {
                var person = dto.CreateInstance<Domain.Models.Person>();
                var data = JsonData(person);
                var result = Task.Run(async () => await Client.PostAsync(PersonEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Person Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the person with id (.*) complete the form with required fields and click the Update button")]
        public void WhenThePersonWithIdCompleteTheFormWithRequiredFieldsAndClickTheUpdateButton(int personId, Table dto)
        {
            try
            {
                var person = dto.CreateInstance<Domain.Models.Person>();
                var data = JsonData(person);
                var result = Task.Run(async () => await Client.PutAsync($"{PersonEndpoint}/{personId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update Person Integration Test Completed");
                //var personToCompare = ObjectData<Domain.Models.Person>(result.Content.ReadAsStringAsync().Result);
                //Assert.IsTrue(dto.IsEquivalentToInstance(personToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                        /*SCENARY 3*/
        /**************************************************/

        [When(@"the administrator goes to Persons Page, person list should return")]
        public void WhenTheAdministratorGoesToPersonsPagePersonListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(PersonEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Persons Integration Test Completed");
            var persons = ObjectData<List<Domain.Models.Person>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == persons.Count, "Input and Out person count matched");
        }



        /**************************************************/
                        /*SCENARY 4*/
        /**************************************************/

        [When(@"the person with id (.*) goes to Profile Page")]
        public void WhenThePersonWithIdGoesToProfilePage(int personId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/{personId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Person by Id Integration Test Completed");
        }

        [Then(@"person details should be")]
        public void ThenPersonDetailsShouldBe(Table dto)
        {
            var person = dto.CreateInstance<Domain.Models.Person>();
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/{person.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Person Details Integration Test Completed");
            //var personToCompare = ObjectData<Domain.Models.Person>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(personToCompare));
        }



        /**************************************************/
                        /*SCENARY 5*/
        /**************************************************/

        [When(@"the person send a friend request to email ""(.*)""")]
        public void WhenThePersonSendAFriendRequestToEmail(string personEmail)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/email/{personEmail}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Person by Email Integration Test Completed");
        }

        [Then(@"the receiving person details should be")]
        public void ThenTheReceivingPersonDetailsShouldBe(Table dto)
        {
            var person = dto.CreateInstance<Domain.Models.Person>();
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/email/{person.Email}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Person Details Integration Test Completed");
            //var personToCompare = ObjectData<Domain.Models.Person>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(personToCompare));
        }



        /**************************************************/
                        /*SCENARY 6*/
        /**************************************************/

        [When(@"the person with id (.*) click the Delete Account button")]
        public void WhenThePersonWithIdClickTheDeleteAccountButton(int personId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/{personId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the person with id (.*) is removed and removed person details should be")]
        public void ThenThePersonWithIdIsRemovedAndRemovedPersonDetailsShouldBe(int personId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{PersonEndpoint}/{personId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Delete Person Integration Test Completed");
                //var personToCompare = ObjectData<Domain.Models.Person>(result.Content.ReadAsStringAsync().Result);
                //Assert.IsTrue(dto.IsEquivalentToInstance(personToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

    }
}
