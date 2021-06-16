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



namespace UltimateTeamApi.SpecFlowTest.Group
{
    [Binding]
    public class GroupSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        private string GroupEndpoint { get; set; }

        public GroupSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
            GroupEndpoint = $"{ApiUri}api/groups";
        }

        

        /**********************************************************************/
        /*INITIALIZING TEST WITH SOME GROUPS, USERS AND GROUPMEMBERS INSTANCES*/
        /**********************************************************************/

        [When(@"groups required attributes provided to initialize instances")]
        public void WhenGroupsRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            //Creating some groups
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var group = row.CreateInstance<Domain.Models.Group>();
                    var data = JsonData(group);
                    var result = Task.Run(async () => await Client.PostAsync(GroupEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Group Integration Test Completed");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }
        }
        
        //[When(@"users required attributes provided to initialize instances")]
        //This function is on the UserSteps.cs

        [Then(@"assign the user with Id (.*) on the group with Id (.*)")]
        public void ThenAssignTheUserWithIdOnTheGroupWithId(int userId, int groupId, Table dto)
        {
            try
            {
                var groupMember = dto.CreateInstance<UltimateTeamApi.Domain.Models.GroupMember>();
                var data = JsonData(groupMember);
                var result = Task.Run(async () => await Client.PostAsync($"{GroupMemberEndpoint(groupMember.UserId)}/{groupMember.GroupId}", data)).Result;
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
        
        [When(@"the user complete the form with the required fields and click the Create button")]
        public void WhenTheUserCompleteTheFormWithTheRequiredFieldsAndClickTheCreateButton(Table dto)
        {
            try
            {
                var group = dto.CreateInstance<UltimateTeamApi.Domain.Models.Group>();
                var data = JsonData(group);
                var result = Task.Run(async () => await Client.PostAsync(GroupEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Group Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }

        }
        
       


        /**************************************************/
        /*SCENARY 2*/
        /**************************************************/
        [When(@"the user complete the form to update the group with Id (.*) and click the Update button")]
        public void WhenTheUserCompleteTheFormToUpdateTheGroupWithIdAndClickTheUpdateButton(int groupId, Table dto)
        {
            try
            {
                var group = dto.CreateInstance<Domain.Models.Group>();
                var data = JsonData(group);
                var result = Task.Run(async () => await Client.PutAsync($"{GroupEndpoint}/{groupId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update Group Integration Test Completed");
                var groupToCompare = ObjectData<Domain.Models.Group>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(groupToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }



        /**************************************************/
        /*SCENARY 3*/
        /**************************************************/
        
        [When(@"the administrator goes to Groups Page, group list should return")]
        public void WhenTheAdministratorGoesToGroupsPageGroupListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(GroupEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Groups Integration Test Completed");
            var groups = ObjectData<List<Domain.Models.Group>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == groups.Count, "Input and Out Group count matched");
        }



        /**************************************************/
        /*SCENARY 4*/
        /**************************************************/

        [When(@"the user goes to Group Lists and click on group with id (.*)")]
        public void WhenTheUserGoesToGroupListsAndClickOnGroupWithId(int groupId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Group by Id Integration Test Completed");
        }

        [Then(@"group details should be")]
        public void ThenGroupDetailsShouldBe(Table dto)
        {
            var group = dto.CreateInstance<Domain.Models.Group>();
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{group.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Group Details Integration Test Completed");
            var groupToCompare = ObjectData<Domain.Models.Group>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(groupToCompare));
        }



        /**************************************************/
        /*SCENARY 5*/
        /**************************************************/
        [When(@"the user goes to Group Lists and click on the group with Id (.*) and go to member list")]
        public void WhenTheUserGoesToGroupListsAndClickOnTheGroupWithId(int groupId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}/groupMembers")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Members by GroupId Integration Test Completed");
        }

        [Then(@"the member list of group with Id (.*) should be")]
        public void ThenTheMemberListOfGroupWithIdShouldBe(int groupId, Table dto)
        {
            var users = dto.CreateInstance<List<Domain.Models.User>>();
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}/groupMembers")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Members Details Integration Test Completed");
            var usersToCompare = ObjectData<List<Domain.Models.User>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(usersToCompare));
        }



        /**************************************************/
        /*SCENARY 6*/
        /**************************************************/

        [When(@"the user with id (.*) click the Delete Group button")]
        public void WhenTheUserWithIdClickTheDeleteGroupButton(int groupId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{GroupEndpoint}/{groupId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }

        [Then(@"the user with id (.*) is removed and removed group details should be")]
        public void ThenTheUserWithIdIsRemovedAndRemovedGroupDetailsShouldBe(int groupId, Table dto)
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
