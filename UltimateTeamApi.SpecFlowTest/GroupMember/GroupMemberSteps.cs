using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.SpecFlowTest.GroupMember
{
    [Binding]
    public class GroupMemberSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        private string GroupEndpoint { get; set; }

        public GroupMemberSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
            GroupEndpoint = $"{ApiUri}api/groups";
        }

        /**********************************************************************/
        /*INITIALIZING TEST WITH SOME GROUPS, USERS AND GROUPMEMBERS INSTANCES*/
        /**********************************************************************/

        //[When(@"groups required attributes provided to initialize instances")]
        //This function is on the GroupSteps.cs

        //[When(@"users required attributes provided to initialize instances")]
        //This function is on the UserSteps.cs

        [Then(@"assign the group with Id (.*) with the user with Id (.*)")]
        public void ThenAssignTheGroupWithIdWithTheUserWithId(int userId, int groupId, Table dto)
        {
            try
            {
                var groupMember = dto.CreateInstance<UltimateTeamApi.Domain.Models.GroupMember>();
                var data = JsonData(groupMember);
                var result = Task.Run(async () => await Client.PostAsync($"{GroupMemberEndpoint(userId)}/{groupId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save GroupMember Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }


        /**************************************************/
        /*SCENARY 1*/
        /**************************************************/

        [When(@"the user with id (.*) goes to Profile Page and click on the groups list")]
        public void WhenTheUserWithIdGoesToProfilePageAndClickOnTheGroupsList(int userId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupMemberEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Groups by UserId Integration Test Completed");
        }

        [Then(@"the groups list of user with Id (.*) should be")]
        public void ThenTheGroupsListOfUserWithIdShouldBe(int userId, Table dto)
        {
            var groups = dto.CreateInstance<List<Domain.Models.Group>>();
            var result = Task.Run(async () => await Client.GetAsync($"{GroupMemberEndpoint(userId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Groups Details Integration Test Completed");
            //var groupsToCompare = ObjectData<List<Domain.Models.Group>>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(groupsToCompare));
        }



        /**************************************************/
        /*SCENARY 2*/
        /**************************************************/

        //[When(@"the user complete the form with the required fields and click the Create button")]
        //This function is on the GroupSteps.cs

        [Then(@"it is assigned to the user with Id (.*) on the group with Id (.*) and list groups should be")]
        public void ThenItIsAssignedToTheUserWithIdOnTheGroupWithIdAndListGroupsShouldBe(int userId, int groupId, Table dto)
        {
            try
            {
                var groupMember = dto.CreateInstance<UltimateTeamApi.Domain.Models.GroupMember>();
                var data = JsonData(groupMember);
                var result = Task.Run(async () => await Client.PostAsync($"{GroupMemberEndpoint(userId)}/{groupId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save GroupMember Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
        /*SCENARY 3*/
        /**************************************************/

        [When(@"the user with id (.*) click the Leave Group button")]
        public void WhenTheUserWithIdClickTheLeaveGroupButton(int groupId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the user with id (.*) leaves the group and removed group details should be")]
        public void ThenTheUserWithIdLeavesTheGroupAndRemovedGroupDetailsShouldBe(int groupId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{GroupEndpoint}/{groupId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Delete Group Integration Test Completed");
                var groupToCompare = ObjectData<Domain.Models.Group>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(groupToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        private string GroupMemberEndpoint(int userId)
        {
            return $"{ApiUri}api/users/{userId}/groups";
        }
    }
}
