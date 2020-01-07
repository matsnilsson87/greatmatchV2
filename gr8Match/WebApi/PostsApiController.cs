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
        public string WrittenByName(int id) {
            string name = ctx.Users.SqlQuery("select FirstName + ' ' + LastName from Users where Id = " + id).ToString();
            return name;
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
