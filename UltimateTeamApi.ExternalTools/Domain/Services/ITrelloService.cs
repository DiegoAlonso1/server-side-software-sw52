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
        Task<TrelloAccountResponse> AssignToken();
        Task<TrelloAccountResponse> UnassignToken();


        //MEMBER
        Task<TrelloMemberResponse> GetMemberByIdAsync(string memberId);
        Task<IEnumerable<TrelloMemberResource>> GetAllMembersByCardIdAsync(string cardId);

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
        Task<TrelloListResponse> GetListByCardIdAsync(string cardId);
        Task<TrelloListResponse> GetListByIdAsync(string listId);
        Task<TrelloListResponse> SaveListOnABoardAsync(SaveTrelloListResource resource, string boardId);
        Task<TrelloListResponse> UpdateListOnABoardAsync(string listId, SaveTrelloListResource resource);

        //ORGANIZATIONS

        Task<IEnumerable<TrelloOrganizationResource>> GetAllOrganizationsByMemberIdAsync(string memberId);
        Task<TrelloOrganizationResponse> SaveOrganizationAsync(SaveTrelloOrganizationResource resource);
        Task<TrelloOrganizationResponse> UpdateOrganizationAsync(string organizationId, SaveTrelloOrganizationResource resource);
        Task<TrelloOrganizationResponse> DeleteOrganizationAsync(string organizationId);
    }
}
