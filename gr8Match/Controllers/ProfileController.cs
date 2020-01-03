using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using gr8Match.Models;

namespace gr8Match.Controllers
{
    public class ProfileController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            var ctx = new Gr8DbContext();
            ctx.Users.Add(model);
            ctx.SaveChanges();

            return Redirect(Url.Action("Index", "Image"));
        }
        public int ThisUser()
        {

            var Users = new List<User>();
            var ctx = new Gr8DbContext();
            Users = ctx.Users.ToList();

            int thisUserId = 0;
            foreach (var u in Users)
            {
                if (u.IdentityID == User.Identity.GetUserId())
                {
                    thisUserId = u.Id;
                    Console.WriteLine(thisUserId);
                    return thisUserId;
                }
            }
            return 0;
        }



        public ActionResult MyProfile()
        {
            int id = ThisUser();
            var ctx = new Gr8DbContext();
            var viewModel = new MyProfileViewModel
            {
                MyImage = ctx.Images.Where(i => i.UserId == id).FirstOrDefault(),
                FirstName = ctx.Database.SqlQuery<string>("Select FirstName from Users where Id =" + id ).FirstOrDefault(),
                LastName = ctx.Database.SqlQuery<string>("Select LastName from Users where Id =" + id ).FirstOrDefault(),
                MyInterests = ctx.Database.SqlQuery<string>("select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList(),
                MyPosts = ctx.Posts.Where(i => i.WrittenTo == id).ToList()
            };
            return View(viewModel);
        }

        public ActionResult MyFriends()
        {
            var id = User.Identity.GetUserId();
            var ctx = new Gr8DbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Users = ctx.Database.SqlQuery<User>("Select * From Users Where Users.Id in (Select ToUser From FriendRequests Where FromUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'True')) or Users.Id in (Select FromUser From FriendRequests Where ToUser in (Select Id From Users Where IdentityID = '" + id +"' and Accepted = 'True'))")
                                                    .ToList()
            };
            return View(viewModel);
        }

       


    }
}