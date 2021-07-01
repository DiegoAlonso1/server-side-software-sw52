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
        private string PersonEndpoint { get; set; }
        private string GroupEndpoint { get; set; }

        public GroupMemberSteps()
        {
            PersonEndpoint = $"{ApiUri}api/persons";
            GroupEndpoint = $"{ApiUri}api/groups";
        }

        /**********************************************************************/
        /*INITIALIZING TEST WITH SOME GROUPS, PERSONS AND GROUPMEMBERS INSTANCES*/
        /**********************************************************************/

        //[When(@"groups required attributes provided to initialize instances")]
        //This function is on the GroupSteps.cs

        //[When(@"persons required attributes provided to initialize instances")]
        //This function is on the PersonSteps.cs

        /**************************************************/
                            /*SCENARY 1*/
        /**************************************************/

        [When(@"the person with id (.*) goes to Profile Page and click on the groups list")]
        public void WhenThePersonWithIdGoesToProfilePageAndClickOnTheGroupsList(int personId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupMemberEndpoint(personId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Groups by PersonId Integration Test Completed");
        }

        [Then(@"the groups list of person with Id (.*) should be")]
        public void ThenTheGroupsListOfPersonWithIdShouldBe(int personId, Table dto)
        {
            var groups = dto.CreateInstance<List<Domain.Models.Group>>();
            var result = Task.Run(async () => await Client.GetAsync($"{GroupMemberEndpoint(personId)}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Groups Details Integration Test Completed");
            //var groupsToCompare = ObjectData<List<Domain.Models.Group>>(result.Content.ReadAsStringAsync().Result);
            //Assert.IsTrue(dto.IsEquivalentToInstance(groupsToCompare));
        }



        /**************************************************/
                            /*SCENARY 2*/
        /**************************************************/

        //[When(@"the person complete the form with the required fields and click the Create button")]
        //This function is on the GroupSteps.cs

        [Then(@"it is assigned to the person with Id (.*) on the group with Id (.*) and list groups should be")]
        public void ThenItIsAssignedToThePersonWithIdOnTheGroupWithIdAndListGroupsShouldBe(int personId, int groupId, Table dto)
        {
            try
            {
                var groups = dto.CreateInstance<List<Domain.Models.Group>>();
                var groupMember = new Resources.SaveGroupMemberResource{ PersonCreator = true };
                var data = JsonData(groupMember);
                var result = Task.Run(async () => await Client.PostAsync($"{GroupMemberEndpoint(personId)}/{groupId}", data)).Result;
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

        [When(@"the person with id (.*) click the Leave Group button")]
        public void WhenThePersonWithIdClickTheLeaveGroupButton(int groupId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the person with id (.*) leaves the group and removed group details should be")]
        public void ThenThePersonWithIdLeavesTheGroupAndRemovedGroupDetailsShouldBe(int groupId, Table dto)
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



        private string GroupMemberEndpoint(int personId)
        {
            return $"{ApiUri}api/persons/{personId}/groups";
        }
    }
}
