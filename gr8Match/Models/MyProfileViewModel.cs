using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gr8Match.Models;

namespace gr8Match.Models
{
    public class MyProfileViewModel
    {
        public User MyUser { get; set; }
       
        public List<string> MyInterests { get; set; }
        
        public List<Posts> MyPosts { get; set; }

        public MyProfileViewModel()
        {
            MyUser = new User();
            MyInterests = new List<string>();
            MyPosts = new List<Posts>();
        }
    }

}   