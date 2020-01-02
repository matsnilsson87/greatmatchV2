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

            return RedirectToAction("Index", "UserImages");
        }

     


        public ActionResult MyProfile()
        {
            return View();
        }

        public ActionResult MyFriends()
        {
            var ctx = new Gr8DbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Users = ctx.Database.SqlQuery<User>("Select * From Users Join FriendRequests on Users.Id = FriendRequests.ToUser Where FriendRequests.FromUser = 3").ToList()
            };
            return View(viewModel);
        }

       
    }
}