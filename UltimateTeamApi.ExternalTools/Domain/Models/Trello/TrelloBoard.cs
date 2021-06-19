using System;
using System.Collections.Generic;
using UltimateTeamApi.ExternalTools.Domain.Models.Trello;

namespace UltimateTeamApi.ExternalTools.Domain.Models.Trello
{
    public class TrelloBoard
    {
        public string name { get; set; }
        public string desc { get; set; }
        public object descData { get; set; }
        public bool closed { get; set; }
        public DateTime? dateClosed { get; set; }
        public string idOrganization { get; set; }
        public object idEnterprise { get; set; }
        public object limits { get; set; }
        public object pinned { get; set; }
        public string shortLink { get; set; }
        public List<object> powerUps { get; set; }
        public DateTime? dateLastActivity { get; set; }
        public List<object> idTags { get; set; }
        public object datePluginDisable { get; set; }
        public string creationMethod { get; set; }
        public object ixUpdate { get; set; }
        public bool enterpriseOwned { get; set; }
        public object idBoardSource { get; set; }
        public string idMemberCreator { get; set; }
        public string id { get; set; }
        public bool starred { get; set; }
        public string url { get; set; }
        public Prefs1 prefs { get; set; }
        public bool subscribed { get; set; }
        public LabelNames labelNames { get; set; }
        public DateTime dateLastView { get; set; }
        public string shortUrl { get; set; }
        public object templateGallery { get; set; }
        public List<object> premiumFeatures { get; set; }
        public List<Membership> memberships { get; set; }
    }
}
    