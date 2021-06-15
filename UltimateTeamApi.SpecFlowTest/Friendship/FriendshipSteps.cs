using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UltimateTeamApi.SpecFlowTest.Friendship
{
    [Binding]
    public class FriendshipSteps : BaseTest
    {
        private string UserEndpoint { get; set; }

        public FriendshipSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
        }


        /**************************************************/
        /*INITIALIZING TEST WITH SOME ENTITIES INSTANCES*/
        /**************************************************/

        //[When(@"users required attributes provided to initialize instances")]
        //This function is on the UserSteps.cs

        [Then(@"assign the user with Id (.*) with the user with Id (.*)")]
        public void ThenAssignTheUserWithIdWithTheUserWithId(int user1Id, int user2Id)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{FriendshipEndpoint(user1Id)}/{user2Id}", null)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Assign Friendship Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
                throw;
            }
        }



        /**************************************************/
                           /*SCENARY 1*/
        /**************************************************/

        [When(@"the user with Id (.*) goes to Friend Lists")]
        public void WhenTheUserWithIdGoesToFriendLists(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{userId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get User by Id Integration Test Completed");
        }

        [Then(@"the friend list of user with Id (.*) should be")]
        public void ThenTheFriendListOfUserWithIdShouldBe(int userId, Table dto)
        {
            var users = dto.CreateInstance<List<Domain.Models.User>>();
            var result = Task.Run(async () => await Client.GetAsync($"{FriendshipEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Friends Details Integration Test Completed");
            var usersToCompare = ObjectData<List<Domain.Models.User>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(usersToCompare));
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the user with Id (.*) accepts the friend request from the user with Id (.*), the user details should be")]
        public void WhenTheUserWithIdAcceptsTheFriendRequestFromTheUserWithIdTheUserDetailsShouldBe(int user2Id, int user1Id, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{FriendshipEndpoint(user1Id)}/{user2Id}", null)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Assign Friendship Integration Test Completed");
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

        [When(@"the user with id (.*) click on the Trash can button next to the user with Id (.*)")]
        public void WhenTheUserWithIdClickOnTheTrashCanButtonNextToTheUserWithId(int user1Id, int user2Id)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{UserEndpoint}/{user1Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the user with id (.*) is removed from the friend list of the user with Id (.*) and removed user details should be")]
        public void ThenTheUserWithIdIsRemovedFromTheFriendListOfTheUserWithIdAndRemovedUserDetailsShouldBe(int user2Id, int user1Id, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{FriendshipEndpoint(user1Id)}/{user2Id}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Unassing Friendship Integration Test Completed");
                var userToCompare = ObjectData<Domain.Models.User>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(userToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }




        private string FriendshipEndpoint(int user1Id)
        {
            return $"{ApiUri}api/users/{user1Id}/friends";
        }
    }
}
