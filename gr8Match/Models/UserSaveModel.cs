using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class UserSaveModel
    {
        public User MyUser { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string ProfileImage { get; set; }

        public bool Active { get; set; }
        public string IdentityID { get; set; }
    }
}