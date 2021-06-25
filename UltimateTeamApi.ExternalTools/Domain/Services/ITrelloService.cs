using System;
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

        // BOARD
        Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId);
        Task<TrelloBoardResponse> GetBoardByIdAsync(string boardId);
        Task<TrelloBoardResponse> SaveBoardAsync(SaveTrelloBoardResource resource);
        Task<TrelloBoardResponse> UpdateBoardAsync(string boardId, SaveTrelloBoardResource resource);
        Task<TrelloBoardResponse> DeleteBoardAsync(string boardId);

        // CARDS
        Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId);
        Task<TrelloCardResponse> GetCardByIdAsync(string cardId);
        //Task<TrelloCardResponse> SaveCardAsync(SaveTrelloCardResource resource);
        Task<TrelloCardResponse> UpdateCardAsync(string cardId, SaveTrelloCardResource resource);
        Task<TrelloCardResponse> DeleteCardAsync(string cardId);

        //LIST

        Task<IEnumerable<TrelloListResource>> GetAllListsByBoardIdAsync(string boardId);
        Task<TrelloListResponse> GetListByIdAsync(string listId);
        Task<TrelloListResponse> SaveListOnABoardAsync(SaveTrelloListResource resource, string boardId);
        Task<TrelloListResponse> UpdateListOnABoardAsync(string listId, SaveTrelloListResource resource);
        
    }
}
