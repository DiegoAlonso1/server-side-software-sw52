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
        TrelloAuthenticationResponse AssignToken(string accessToken);

        //MEMBER
        Task<TrelloMemberResponse> GetMemberByIdAsync(string memberId, string accessToken);
        Task<IEnumerable<TrelloMemberResource>> GetAllMembersByCardIdAsync(string cardId, string accessToken);

        // BOARD
        Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId, string accessToken);
        Task<TrelloBoardResponse> GetBoardByIdAsync(string boardId, string accessToken);
        Task<TrelloBoardResponse> SaveBoardAsync(SaveTrelloBoardResource resource, string accessToken);
        Task<TrelloBoardResponse> UpdateBoardAsync(string boardId, SaveTrelloBoardResource resource, string accessToken);
        Task<TrelloBoardResponse> DeleteBoardAsync(string boardId, string accessToken);

        // CARDS
        Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId, string accessToken);
        Task<TrelloCardResponse> GetCardByIdAsync(string cardId, string accessToken);
        //Task<TrelloCardResponse> SaveCardAsync(SaveTrelloCardResource resource);
        Task<TrelloCardResponse> UpdateCardAsync(string cardId, SaveTrelloCardResource resource, string accessToken);
        Task<TrelloCardResponse> DeleteCardAsync(string cardId, string accessToken);

        //LIST

        Task<IEnumerable<TrelloListResource>> GetAllListsByBoardIdAsync(string boardId, string accessToken);
        Task<TrelloListResponse> GetListByCardIdAsync(string cardId, string accessToken);
        Task<TrelloListResponse> GetListByIdAsync(string listId, string accessToken);
        Task<TrelloListResponse> SaveListOnABoardAsync(SaveTrelloListResource resource, string boardId, string accessToken);
        Task<TrelloListResponse> UpdateListOnABoardAsync(string listId, SaveTrelloListResource resource, string accessToken);

        //ORGANIZATIONS

        Task<IEnumerable<TrelloOrganizationResource>> GetAllOrganizationsByMemberIdAsync(string memberId, string accessToken);
        Task<TrelloOrganizationResponse> SaveOrganizationAsync(SaveTrelloOrganizationResource resource, string accessToken);
        Task<TrelloOrganizationResponse> UpdateOrganizationAsync(string organizationId, SaveTrelloOrganizationResource resource, string accessToken);
        Task<TrelloOrganizationResponse> DeleteOrganizationAsync(string organizationId, string accessToken);
    }
}
