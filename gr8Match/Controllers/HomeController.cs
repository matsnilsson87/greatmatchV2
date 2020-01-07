using gr8Match.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gr8Match.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Users = new List<User>();
            var ctx = new Gr8DbContext();
            Users = ctx.Users.ToList();
            var _user =  new User();

            foreach (var u in Users)
            {
                if (u.IdentityID == User.Identity.GetUserId())
                {
                    _user = u;
                }
            }

            var viewModel = new UserViewModel(_user)
            {
                Profiles = ctx.Database.SqlQuery<User>("select top 5 * from Users order by newid()").ToList()

            };


            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}