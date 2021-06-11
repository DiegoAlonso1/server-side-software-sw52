﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class SessionStadistic
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int FunctionalityId { get; set; }
        public Functionality Functionality { get; set; }
        public int Count { get; set; }
    }
}
