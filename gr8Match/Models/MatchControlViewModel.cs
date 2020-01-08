using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class MatchControlViewModel
    {
        public User OtherUser { get; set; }
        public List<string> Interests { get; set; }

        public MatchControlViewModel()
        {
            Interests = new List<string>();
            OtherUser = new User();
        }
    }
}

    