using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Models.Trello;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface ITrelloService
    {
        Task<TrelloMemberResponse> GetMemberById(string memberId);
        Task<TrelloAccountResponse> AssignToken();
        Task<TrelloAccountResponse> UnassignToken();
        //Task<TrelloBoardResponse> GetAllBoardsByMemberId(string memberId);
        Task<TrelloBoardResponse> GetBoardById(string boardId);
        
    }
}
