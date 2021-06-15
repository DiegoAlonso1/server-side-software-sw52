﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.8.0.0
//      SpecFlow Generator Version:3.8.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UltimateTeamApi.SpecFlowTest.User
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class UserFeature : object, Xunit.IClassFixture<UserFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "User.feature"
#line hidden
        
        public UserFeature(UserFeature.FixtureData fixtureData, UltimateTeamApi_SpecFlowTest_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "User", "User", "\tCreate, Update, Delete and Get a User", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="1 Initialize some User Intances")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "1 Initialize some User Intances")]
        [Xunit.TraitAttribute("Category", "mytag")]
        public virtual void _1InitializeSomeUserIntances()
        {
            string[] tagsOfScenario = new string[] {
                    "mytag"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1 Initialize some User Intances", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table25.AddRow(new string[] {
                            "Sam",
                            "Morales",
                            "ElTioSam",
                            "sam@hotmail.com",
                            "TresNodos",
                            "2002-04-19",
                            "2020-04-19",
                            "null",
                            "1"});
                table25.AddRow(new string[] {
                            "Lucia",
                            "Revollar",
                            "Lulu",
                            "lulu@gmail.com",
                            "CrusUpc#3",
                            "2003-01-09",
                            "2020-01-20",
                            "null",
                            "1"});
                table25.AddRow(new string[] {
                            "Maria",
                            "Santillan",
                            "Maria503",
                            "ma503@yopmail.com",
                            "Password",
                            "2000-07-30",
                            "2018-09-12",
                            "null",
                            "1"});
                table25.AddRow(new string[] {
                            "Lionel",
                            "Messi",
                            "ElMeias",
                            "leo@barzabestclub.com",
                            "elMejorDelMundo",
                            "1212-12-12",
                            "1219-12-12",
                            "null",
                            "1"});
#line 8
 testRunner.When("users required attributes provided to initialize instances", ((string)(null)), table25, "When ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The user wants to register")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The user wants to register")]
        public virtual void TheUserWantsToRegister()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user wants to register", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table26.AddRow(new string[] {
                            "Fernan",
                            "Floo",
                            "Fernanfloo",
                            "fernan@elcrack.es",
                            "Contraseña",
                            "1999-05-21",
                            "2020-03-20",
                            "null",
                            "1"});
#line 18
 testRunner.When("the user complete the form with the required fields and click the Register button" +
                        "", ((string)(null)), table26, "When ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The user wants to update their data profile")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The user wants to update their data profile")]
        public virtual void TheUserWantsToUpdateTheirDataProfile()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user wants to update their data profile", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 24
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table27 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table27.AddRow(new string[] {
                            "Fernando",
                            "Firulais",
                            "FernanElCrack",
                            "fernan@elcrack.es",
                            "NuevaContraseña",
                            "1999-05-21",
                            "2020-05-20",
                            "null",
                            "1"});
#line 25
 testRunner.When("the user with id 5 complete the form with required fields and click the Update bu" +
                        "tton", ((string)(null)), table27, "When ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The administrator wants to see all users")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The administrator wants to see all users")]
        public virtual void TheAdministratorWantsToSeeAllUsers()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The administrator wants to see all users", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table28 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table28.AddRow(new string[] {
                            "1",
                            "Sam",
                            "Morales",
                            "ElTioSam",
                            "sam@hotmail.com",
                            "TresNodos",
                            "2002-04-19",
                            "2020-04-19",
                            "null",
                            "1"});
                table28.AddRow(new string[] {
                            "2",
                            "Lucia",
                            "Revollar",
                            "Lulu",
                            "lulu@gmail.com",
                            "CrusUpc#3",
                            "2003-01-09",
                            "2020-01-20",
                            "null",
                            "1"});
                table28.AddRow(new string[] {
                            "3",
                            "Maria",
                            "Santillan",
                            "Maria503",
                            "ma503@yopmail.com",
                            "Password",
                            "2000-07-30",
                            "2018-09-12",
                            "null",
                            "1"});
                table28.AddRow(new string[] {
                            "4",
                            "Lionel",
                            "Messi",
                            "ElMeias",
                            "leo@barzabestclub.com",
                            "elMejorDelMundo",
                            "1212-12-12",
                            "1219-12-12",
                            "null",
                            "1"});
                table28.AddRow(new string[] {
                            "5",
                            "Fernando",
                            "Firulais",
                            "FernanElCrack",
                            "fernan@elcrack.es",
                            "NuevaContraseña",
                            "1999-05-21",
                            "2020-05-20",
                            "null",
                            "1"});
#line 32
 testRunner.When("the administrator goes to Users Page, user list should return", ((string)(null)), table28, "When ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The user wants to see his profile data")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The user wants to see his profile data")]
        public virtual void TheUserWantsToSeeHisProfileData()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user wants to see his profile data", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 42
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 43
 testRunner.When("the user with id 5 goes to Profile Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table29 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table29.AddRow(new string[] {
                            "5",
                            "Fernando",
                            "Firulais",
                            "FernanElCrack",
                            "fernan@elcrack.es",
                            "NuevaContraseña",
                            "1999-05-21",
                            "2020-05-20",
                            "null",
                            "1"});
#line 44
 testRunner.Then("user details should be", ((string)(null)), table29, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The user wants to send a friend request")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The user wants to send a friend request")]
        public virtual void TheUserWantsToSendAFriendRequest()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user wants to send a friend request", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 50
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 51
 testRunner.When("the user send a friend request to email \"lulu@gmail.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table30 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table30.AddRow(new string[] {
                            "2",
                            "Lucia",
                            "Revollar",
                            "Lulu",
                            "lulu@gmail.com",
                            "CrusUpc#3",
                            "2003-01-09",
                            "2020-01-20",
                            "null",
                            "1"});
#line 52
 testRunner.Then("the receiving user details should be", ((string)(null)), table30, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="The user wants to delete his account")]
        [Xunit.TraitAttribute("FeatureTitle", "User")]
        [Xunit.TraitAttribute("Description", "The user wants to delete his account")]
        public virtual void TheUserWantsToDeleteHisAccount()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user wants to delete his account", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 58
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 59
 testRunner.When("the user with id 5 click the Delete Account button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table31 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "LastName",
                            "UserName",
                            "Email",
                            "Password",
                            "Birthdate",
                            "LastConnection",
                            "ProfilePicture",
                            "AdministratorId"});
                table31.AddRow(new string[] {
                            "5",
                            "Fernando",
                            "Firulais",
                            "FernanElCrack",
                            "fernan@elcrack.es",
                            "NuevaContraseña",
                            "1999-05-21",
                            "2020-05-20",
                            "null",
                            "1"});
#line 60
 testRunner.Then("the user with id 5 is removed and removed user details should be", ((string)(null)), table31, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                UserFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                UserFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
