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
    2, and so on. Don't run them in the order they appear in the test explorer.*/

namespace UltimateTeamApi.SpecFlowTest.User
{
    [Binding]
    public class UserSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        private string AdministratorEndpoint { get; set; }

        public UserSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
            AdministratorEndpoint = $"{ApiUri}api/administrators";
        }



        /**************************************************/
            /*INITIALIZING TEST WITH SOME USERS INSTANCES*/
        /**************************************************/

        [When(@"users required attributes provided to initialize instances")]
        public void WhenUsersRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            //Ensure there is an administrator with id 1
            var jsonAdmin = Task.Run(async () => await Client.GetAsync($"{AdministratorEndpoint}/1")).Result;

            if (jsonAdmin == null || jsonAdmin.StatusCode != HttpStatusCode.OK)
            {
                try
                {
                    var admin = new Administrator { Id = 1 };
                    var JsonAdmin = JsonData(admin);
                    Task.Run(async () => await Client.PostAsync(UserEndpoint, JsonAdmin));
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }

            //Creating some users
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var user = row.CreateInstance<Domain.Models.User>();
                    var data = JsonData(user);
                    var result = Task.Run(async () => await Client.PostAsync(UserEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save User Integration Test Completed");
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

        [When(@"the user complete the form with the required fields and click the Register button")]
        public void WhenTheUserCompleteTheFormWithTheRequiredFieldsAndClickTheRegisterButton(Table dto)
        {
            try
            {
                var user = dto.CreateInstance<UltimateTeamApi.Domain.Models.User>();
                var data = JsonData(user);
                var result = Task.Run(async () => await Client.PostAsync(UserEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save User Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the user with id (.*) complete the form with required fields and click the Update button")]
        public void WhenTheUserWithIdCompleteTheFormWithRequiredFieldsAndClickTheUpdateButton(int userId, Table dto)
        {
            try
            {
                var user = dto.CreateInstance<Domain.Models.User>();
                var data = JsonData(user);
                var result = Task.Run(async () => await Client.PutAsync($"{UserEndpoint}/{userId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update User Integration Test Completed");
                var userToCompare = ObjectData<Domain.Models.User>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(userToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
                        /*SCENARY 3*/
        /**************************************************/

        [When(@"the administrator goes to Users Page, user list should return")]
        public void WhenTheAdministratorGoesToUsersPageUserListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(UserEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Users Integration Test Completed");
            var users = ObjectData<List<Domain.Models.User>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == users.Count, "Input and Out user count matched");
        }



        /**************************************************/
                        /*SCENARY 4*/
        /**************************************************/

        [When(@"the user with id (.*) goes to Profile Page")]
        public void WhenTheUserWithIdGoesToProfilePage(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{userId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get User by Id Integration Test Completed");
        }

        [Then(@"user details should be")]
        public void ThenUserDetailsShouldBe(Table dto)
        {
            var user = dto.CreateInstance<Domain.Models.User>();
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{user.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "User Details Integration Test Completed");
            var userToCompare = ObjectData<Domain.Models.User>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(userToCompare));
        }



        /**************************************************/
                        /*SCENARY 5*/
        /**************************************************/

        [When(@"the user send a friend request to email ""(.*)""")]
        public void WhenTheUserSendAFriendRequestToEmail(string email)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/email={email}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get User by Email Integration Test Completed");
        }

        [Then(@"the receiving user details should be")]
        public void ThenTheReceivingUserDetailsShouldBe(Table dto)
        {
            var user = dto.CreateInstance<Domain.Models.User>();
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/email={user.Email}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "User Details Integration Test Completed");
            var userToCompare = ObjectData<Domain.Models.User>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(userToCompare));
        }



        /**************************************************/
                        /*SCENARY 6*/
        /**************************************************/

        [When(@"the user with id (.*) click the Delete Account button")]
        public void WhenTheUserWithIdClickTheDeleteAccountButton(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{userId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the user with id (.*) is removed and removed user details should be")]
        public void ThenTheUserWithIdIsRemovedAndRemovedUserDetailsShouldBe(int userId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{UserEndpoint}/{userId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Delete User Integration Test Completed");
                var userToCompare = ObjectData<Domain.Models.User>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(userToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

    }
}
