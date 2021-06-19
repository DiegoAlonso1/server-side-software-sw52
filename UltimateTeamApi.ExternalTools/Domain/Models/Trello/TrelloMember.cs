using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Models.Trello
{
    public class TrelloMember
    {
        public string id { get; set; }
        public string bio { get; set; }
        public object bioData { get; set; }
        public bool confirmed { get; set; }
        public string memberType { get; set; }
        public string username { get; set; }
        public string aaId { get; set; }
        public bool activityBlocked { get; set; }
        public string avatarHash { get; set; }
        public string avatarUrl { get; set; }
        public string fullName { get; set; }
        public object idEnterprise { get; set; }
        public IList<object> idEnterprisesDeactivated { get; set; }
        public object idMemberReferrer { get; set; }
        public IList<object> idPremOrgsAdmin { get; set; }
        public string initials { get; set; }
        public NonPublic nonPublic { get; set; }
        public bool nonPublicAvailable { get; set; }
        public IList<object> products { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public object aaBlockSyncUntil { get; set; }
        public object aaEmail { get; set; }
        public object aaEnrolledDate { get; set; }
        public string avatarSource { get; set; }
        public int credentialsRemovedCount { get; set; }
        public object domainClaimed { get; set; }
        public string email { get; set; }
        public string gravatarHash { get; set; }
        public IList<string> idBoards { get; set; }
        public IList<string> idOrganizations { get; set; }
        public IList<object> idEnterprisesAdmin { get; set; }
        public IList<string> loginTypes { get; set; }
        public MarketingOptIn marketingOptIn { get; set; }
        public IList<MessagesDismissed> messagesDismissed { get; set; }
        public IList<string> oneTimeMessagesDismissed { get; set; }
        public Prefs prefs { get; set; }
        public IList<object> trophies { get; set; }
        public object uploadedAvatarHash { get; set; }
        public object uploadedAvatarUrl { get; set; }
        public IList<object> premiumFeatures { get; set; }
        public bool isAaMastered { get; set; }
        public string ixUpdate { get; set; }
        public Limits limits { get; set; }

    }
}
