using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class TrelloCardResource
    {
        public string Id { get; set; }
        public DateTime DateLastActivity { get; set; }
        public string BoardId { get; set; }
        public string ListId { get; set; }
        public int ShortId { get; set; }
        public string Name { get; set; }
        public int Pos { get; set; }
        public string ShortLink { get; set; }
        public IEnumerable<object> MembersIds { get; set; }
        public string ShortUrl { get; set; }
        public string Url { get; set; }
    }
}
