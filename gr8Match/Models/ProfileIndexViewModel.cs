using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace gr8Match.Models
{
    public class ProfileIndexViewModel
    {
        public List<User> Users { get; set; }

        public List<User> FriendsRequests { get; set; }
    }

  
}