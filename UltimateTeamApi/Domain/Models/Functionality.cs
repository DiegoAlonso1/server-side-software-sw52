using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Functionality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SessionStadistic> SessionStadistics { get; set; }
    }
}
