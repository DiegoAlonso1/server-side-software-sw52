using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Resources
{
    public class DriveFileResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Extension { get; set; }
        public string Kind { get; set; }
        public IList<DriveUserResource> Owners { get; set; }
        public IList<string> FolderParentsIds { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public string DownloadLink { get; set; }
        public string ViewLink { get; set; }
    }
}
