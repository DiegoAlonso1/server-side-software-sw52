using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class MiroBoardResource
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public static DateTime CreatedAt { get; }
        public DateTime ModifiedAt { get; set; }
        public MiroUserResource CreatedBy { get; set; }
        public MiroUserResource ModifiedBy { get; set; }
        public MiroUserResource Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ViewLink{ get; set; }
        public object SharingPolicy { get; set; }
        public string SharingPolicyAccess { get; set; }
        public string SharingPolicyTeamAccess { get; set; }
        public MiroUserConnectionResource currentUserConnection { get; set; }
    }
}
