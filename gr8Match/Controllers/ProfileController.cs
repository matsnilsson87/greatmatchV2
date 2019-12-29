using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using gr8Match.Models;

namespace gr8Match.Controllers
{
    public class ProfileController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var ctx = new Gr8DbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Users = ctx.Users.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            var ctx = new Gr8DbContext();
            ctx.Users.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
            return View();
        }

        public ActionResult MyFriends()
        {
            return View();
        }
    }
}