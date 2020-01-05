using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gr8Match.Models;

namespace gr8Match.Controllers
{
    public class HelpController : Controller
    {
       
        public static int CountFriendRequests(String _id)
        {
            var id = _id;
            var ctx = new Gr8DbContext();
            int count = ctx.Database.SqlQuery<int>("select count(*) from FriendRequests where ToUser = (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'False')").Sum();
            
            return count;
        }


    }
}