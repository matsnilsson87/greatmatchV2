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
            try
            {
                var id = _id;
                var ctx = new Gr8DbContext();
                int count = ctx.Database.SqlQuery<int>("select count(*) from FriendRequests where ToUser = (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'False')").Sum();

                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
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
        public static string WhoWroteTheMessage(int Id)
        {
            try
            {
                int friendId = Id;
                var ctx = new Gr8DbContext();
                string name = ctx.Database.SqlQuery<string>("select firstname + ' ' + lastname from users where id = " + friendId).FirstOrDefault();

                return name;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Okänd avsändare";
            }
        }

    }
}