﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface ITrelloService
    {
        Task<TrelloMemberResponse> GetMemberByIdAsync(string memberId);
        Task<TrelloAccountResponse> AssignToken();
        Task<TrelloAccountResponse> UnassignToken();
        Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId);
        Task<TrelloBoardResponse> GetBoardByIdAsync(string boardId);
        Task<TrelloBoardResponse> SaveBoardAsync(SaveTrelloBoardResource resource);
        Task<TrelloBoardResponse> UpdateBoardAsync(string boardId, SaveTrelloBoardResource resource);
    }
}
