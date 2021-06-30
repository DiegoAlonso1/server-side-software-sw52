using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class MiroUserResource
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public string Industry { get; set; }
        public string Email { get; set; }
        public string State{ get; set; }
        public MiroPictureResource Picture { get; set; }
    }
}
