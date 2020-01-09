using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class CategoryViewModel
    {
        public int FriendId { get; set; }
        public List<Category> CategoryList { get; set; }
    }
}