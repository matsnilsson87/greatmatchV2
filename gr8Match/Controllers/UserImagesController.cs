using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gr8Match.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace gr8Match.Controllers
{
    public class UserImagesController : Controller
    {
       [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Image image)

        {       


                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string fileExt = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExt;
                image.ImgPath = "~/UserImage/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/UserImage/") + fileName);
                image.UserId = ThisUser();
                image.ImageFile.SaveAs(fileName);
                using (Gr8DbContext db = new Gr8DbContext())
                {
                db.Images.Add(image);
                db.SaveChanges();
                }
                ModelState.Clear();

            return RedirectToAction("MyProfile", "Profile");
        }

        public int ThisUser()
        {
            var Users = new List<User>();
            int thisUserId = 0;
            foreach (var u in Users)
            {
                if (u.IdentityID == User.Identity.GetUserId())
                {
                    thisUserId = u.Id;
                }
            }
            return thisUserId;
        }


    }
}