using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloListResource
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string Name { get; set; }
        public int Pos { get; set; }
    }
}
