using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.SpecFlowTest.Friendship
{
    [Binding]
    class FriendshipSteps : BaseTest
    {
        private string FriendshipEndpoint { get; set; }

        public FriendshipSteps()
        {
            FriendshipEndpoint = $"{ApiUri}api/friendships";
        }
    }
}
