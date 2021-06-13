using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class GroupMemberResponse : BaseResponse<GroupMember>
    {
        public GroupMemberResponse(GroupMember resource) : base(resource)
        {
        }

        public GroupMemberResponse(string message) : base(message)
        {
        }
    }
}
