using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gr8Match.Models;


namespace gr8Match.WebApi
{
    public class PostsApiController : ApiController
    {
        private readonly Gr8DbContext ctx;

        public PostsApiController()
        {
            ctx = new Gr8DbContext();
        }
        // /api/postsapi/getpostslist
        [HttpGet]
        public PostsViewModel GetPostsList(int id) {


            var view = new PostsViewModel
            {
                PostsList = ctx.Posts.Where(i => i.WrittenTo == id).OrderBy(d => d.Id).ToList()
            };
            
            return view;

        }


        // /api/postsapi/send
        [HttpPost]
        public string Send([FromBody]PostsViewModel model) {
            try
            {
                ctx.Posts.Add(model.Post);
                ctx.SaveChanges();
                return "Meddelandet skickat";

            }
            catch {
                return "Opps, något gick fel ring rudde!";
             
            }
        
        
        }


    }
}
