using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class OtherProfileViewModel
    {
        public User OtherUser { get; set; }

        public List<string> OtherUserInterests { get; set; }

        public int FriendControl { get; set; }

        public int FriendRequestControl { get; set; }



        public OtherProfileViewModel()
        {
            OtherUser = new User();
            OtherUserInterests = new List<string>();
           
        }
    }
}