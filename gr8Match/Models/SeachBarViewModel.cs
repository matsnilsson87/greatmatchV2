using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gr8Match.Models;

namespace gr8Match.Models
{
    public class SeachBarViewModel
    {
        public List<User> User { get; set; }

        public SeachBarViewModel()
        {
            User = new List<User>();
        }
    }
}