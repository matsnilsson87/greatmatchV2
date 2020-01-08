using gr8Match.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gr8Match.WebApi
{
    public class VisitorController : ApiController
    {

        private readonly Gr8DbContext ctx;

        public VisitorController()
        {
            ctx = new Gr8DbContext();
        }
        // /api/visitor/getlatestvisitors
        [HttpGet]
        public IEnumerable<User> GetLatestVisitors(int id)
        {
            
           
            var top5 = ctx.Database.SqlQuery<User>("Select Top 5 Users.* from Users join Visitors on Users.Id = VisitorId Where VisitedProfile =" +id+ " Order by Date desc ").ToList();

            
                return top5;
            }
    }
}
