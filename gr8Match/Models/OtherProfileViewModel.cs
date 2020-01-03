using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class OtherProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Age { get; set; }
        public List<string> MyInterests { get; set; }
        public Image MyImage { get; set; }
        public List<Posts> MyPosts { get; set; }

        public OtherProfileViewModel()
        {
            FirstName = "";
            LastName = "";
            MyInterests = new List<string>();
            MyImage = new Image();
            MyPosts = new List<Posts>();
        }
    }
}