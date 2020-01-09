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
            try
            {
                var Users = new List<User>();
                var ctx = new Gr8DbContext();
                Users = ctx.Users.ToList();
                var _user = new User();

                foreach (var u in Users)
                {
                    if (u.IdentityID == User.Identity.GetUserId())
                    {
                        _user = u;
                    }

                }

                var viewModel = new UserViewModel(_user)
                {
                    Profiles = ctx.Database.SqlQuery<User>("select top 6 * from Users where Active = 'True' and IdentityID != '" + User.Identity.GetUserId() + "' order by newid()").ToList()

                };


                return View(viewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }
    }
}