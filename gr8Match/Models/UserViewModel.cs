﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class UserViewModel
    {
        public User user { get; set; }

        public UserViewModel(User _user)
        {
            user = _user;
        }
    }
}