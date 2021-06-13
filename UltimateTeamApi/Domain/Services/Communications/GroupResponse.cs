using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class GroupResponse : BaseResponse<Group>
    {
        public GroupResponse(Group resource) : base(resource)
        {
        }

        public GroupResponse(string message) : base(message)
        {
        }
    }
}
