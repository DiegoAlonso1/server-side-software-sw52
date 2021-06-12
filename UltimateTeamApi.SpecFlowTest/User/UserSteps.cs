using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

/*****************************************************************************/
                                /*INSTRUCTIONS*/
/*****************************************************************************/

/*1-You must have run the main project at least once without creating any user. 
    Otherwise, if you have created any user, you must delete the database, recreate 
    it and rerun the project so that the IDs match the IDs of these tests. */

/*2-First run the test that initializes some user instances. Then run 1 test at 
    time. In order as the scenarios have been created. Scenario 1 first, then Scenario 
    2, and so on. Don't run them in the order they appear in the test explorer.*/

namespace UltimateTeamApi.SpecFlowTest.User
{
    [Binding]
    public class UserSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        
        public UserSteps()
        {
            UserEndpoint = $"{ApiUri}api/users";
        }



        /**************************************************/
          /*INITIALIZING TEST WITH SOME USERS INSTANCES*/
        /**************************************************/

        [When(@"users required attributes provided to initialize instances")]
        public void WhenUsersRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var user = row.CreateInstance<UltimateTeamApi.Domain.Models.User>();
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
    }
}
