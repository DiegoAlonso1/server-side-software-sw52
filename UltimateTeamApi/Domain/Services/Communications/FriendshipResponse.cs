using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class FriendshipResponse : BaseResponse<Friendship>
    {
        public FriendshipResponse(Friendship resource) : base(resource)
        {
        }

        public FriendshipResponse(string message) : base(message)
        {
        }
    }
}
