using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface ITrelloService
    {
        Task<Object> GetAllBoards();
    }
}
