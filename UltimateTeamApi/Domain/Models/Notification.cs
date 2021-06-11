﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RemitendId { get; set; }
        public User Remitend { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
