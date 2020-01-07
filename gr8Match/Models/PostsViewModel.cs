using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class PostsViewModel
    {
        public List<Posts> PostsList { get; set; }
        public Posts Post { get; set; }

        public PostsViewModel()
        {
            PostsList = new List<Posts>();
        }
        public PostsViewModel(Posts post)
        {
            Post = post;
        }
    }


}