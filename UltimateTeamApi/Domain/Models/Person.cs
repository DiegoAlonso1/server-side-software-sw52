using System;
using System.Collections.Generic;
using System.Drawing;

namespace UltimateTeamApi.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime LastConnection { get; set; }
        public Bitmap ProfilePicture { get; set; }
        public int AdministratorId { get; set; }
        public Administrator Administrator { get; set; }
        public List<SubscriptionBill> SubscriptionBills { get; set; }
        public List<SessionParticipant> SessionsParticipated { get; set; }
        public List<Friendship> FriendShipsAsPrincipal { get; set; }
        public List<Friendship> FriendShipsAsFriend { get; set; }
        public List<Notification> NotificationsSent { get; set; }
        public List<Notification> NotificationsReceived { get; set; }
        public List<GroupMember> GroupMembers { get; set; }
    }
}
