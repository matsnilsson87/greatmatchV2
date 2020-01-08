using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gr8Match.Models;


namespace gr8Match.WebApi
{
    public class PostsController : ApiController
    {
        private readonly Gr8DbContext ctx;

        public PostsController()
        {
            ctx = new Gr8DbContext();
        }
        // /api/posts/getpostslist
        [HttpGet]
        public IEnumerable<Posts> GetPostsList(int id) {

            
                
            var view = new List<Posts>();
            view = ctx.Posts.Where(i => i.WrittenTo == id).OrderBy(d => d.Datum).ToList();
                 
            return view;
        }
        // /api/posts/writtenbyname
        [HttpGet]
        public IHttpActionResult WrittenByName(int id) {

            User thisUser = ctx.Users.SqlQuery("select * from Users where Id = " + id).FirstOrDefault();
         
            return Ok (thisUser);
        }

        // /api/postsapi/send
        [HttpPost]
        public string Send([FromBody]Posts thePost) {
            try
            {
                ctx.Posts.Add(thePost);
                ctx.SaveChanges();
                return "Meddelandet skickat";

            }
            catch {
                return "Opps, något gick fel ring rudde!";
             
            }
        
        
        }


    }
}
