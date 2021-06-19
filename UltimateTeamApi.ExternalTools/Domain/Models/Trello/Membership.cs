namespace UltimateTeamApi.ExternalTools.Domain.Models.Trello
{
    public class Membership
    {
        public string id { get; set; }
        public string idMember { get; set; }
        public string memberType { get; set; }
        public bool unconfirmed { get; set; }
        public bool deactivated { get; set; }
    }
}

