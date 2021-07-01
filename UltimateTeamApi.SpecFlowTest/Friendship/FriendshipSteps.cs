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
        private string PersonEndpoint { get; set; }

        public FriendshipSteps()
        {
            PersonEndpoint = $"{ApiUri}api/persons";
        }


        /**************************************************/
        /*INITIALIZING TEST WITH SOME ENTITIES INSTANCES*/
        /**************************************************/

        //[When(@"persons required attributes provided to initialize instances")]
        //This function is on the PersonSteps.cs

        [Then(@"assign the person with Id (.*) with the person with Id (.*)")]
        public void ThenAssignThePersonWithIdWithThePersonWithId(int principalId, int friendId)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{FriendshipEndpoint(principalId)}/{friendId}", null)).Result;
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
        
        [When(@"the person with Id (.*) goes to Friend Lists")]
        public void WhenThePersonWithIdGoesToFriendLists(int personId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/{personId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Person by Id Integration Test Completed");
        }

        [Then(@"the friend list of person with Id (.*) should be")]
        public void ThenTheFriendListOfPersonWithIdShouldBe(int personId, Table dto)
        {
            var persons = dto.CreateInstance<List<Domain.Models.Person>>();
            var result = Task.Run(async () => await Client.GetAsync($"{FriendshipEndpoint(personId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Friends Details Integration Test Completed");
            //var personsToCompare = ObjectData<List<Domain.Models.Person>>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(personsToCompare));
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        [When(@"the person with Id (.*) accepts the friend request from the person with Id (.*), the person details should be")]
        public void WhenThePersonWithIdAcceptsTheFriendRequestFromThePersonWithIdThePersonDetailsShouldBe(int friendId, int principalId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.PostAsync($"{FriendshipEndpoint(principalId)}/{friendId}", null)).Result;
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

        [When(@"the person with id (.*) click on the Trash can button next to the person with Id (.*)")]
        public void WhenThePersonWithIdClickOnTheTrashCanButtonNextToThePersonWithId(int principalId, int friendId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PersonEndpoint}/{principalId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the person with id (.*) is removed from the friend list of the person with Id (.*) and removed person details should be")]
        public void ThenThePersonWithIdIsRemovedFromTheFriendListOfThePersonWithIdAndRemovedPersonDetailsShouldBe(int friendId, int principalId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{FriendshipEndpoint(principalId)}/{friendId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Unassing Friendship Integration Test Completed");
                //var personToCompare = ObjectData<Domain.Models.Person>(result.Content.ReadAsStringAsync().Result);
                //Assert.IsTrue(dto.IsEquivalentToInstance(personToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }




        private string FriendshipEndpoint(int principalId)
        {
            return $"{ApiUri}api/persons/{principalId}/friends";
        }
    }
}
