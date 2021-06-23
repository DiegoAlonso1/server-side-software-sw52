using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class MiroUserConnectionResource
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public MiroUserResource User { get; set; }
        public static DateTime CreatedAt { get; }
        public DateTime ModifiedAt { get; set; }
        public MiroUserResource CreatedBy { get; set; }
        public MiroUserResource ModifiedBy { get; set; }
    }
}
