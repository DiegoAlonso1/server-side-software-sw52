using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Resources
{
    public class SavePersonResource
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime LastConnection { get; set; }
        public Bitmap ProfilePicture { get; set; }
        public int AdministratorId { get; set; }
    }
}
