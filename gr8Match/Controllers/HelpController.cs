using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gr8Match.Models;
using Microsoft.AspNet.Identity;

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

        public static int ThisUser(string _id)
        {

            var Users = new List<User>();
            var ctx = new Gr8DbContext();
            Users = ctx.Users.ToList();

            int thisUserId = 0;
            foreach (var u in Users)
            {
                if (u.IdentityID == _id)
                {
                    thisUserId = u.Id;
                    Console.WriteLine(thisUserId);
                    return thisUserId;
                }
            }
            return 0;
        }


    }
}