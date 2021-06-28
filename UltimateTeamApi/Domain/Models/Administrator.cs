using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Domain.Models
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Area { get; set; }

        public List<Person> Persons { get; set; }

    }
}
