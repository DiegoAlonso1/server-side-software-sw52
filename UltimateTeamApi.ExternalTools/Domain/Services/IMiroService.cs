using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IMiroService
    {
        Task<MiroAuthenticationResponse> GetAuthAsync(string authCode, string clientId, string teamId);
        Task<MiroBoardResponse> CreateMiroBoardAsync (SaveMiroBoardResource resource, string accessToken);
        Task<MiroBoardResponse> GetMiroBoardByIdAsync(string boardId, string accessToken);
        Task<IEnumerable<MiroBoard4ShareResource>> ShareMiroBoardAsync(string boardId, SaveMiroBoard4ShareResource resource, string accessToken);
        Task<MiroBoardResponse> UpdateMiroBoardAsync(string boardId, SaveMiroBoardResource resource, string accessToken);
        Task<MiroBoardResponse> DeleteMiroBoardbyIdAync(string boardId, string accessToken);
        Task<MiroUserResponse> GetMiroUserByIdAsync(string userId, string accessToken);
        Task<MiroUserResponse> GetMyUserAsync(string accessToken);
    }
}
