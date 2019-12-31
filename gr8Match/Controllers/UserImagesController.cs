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
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Image image)

        {       
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string fileExt = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExt;
                image.ImgPath = "~/Images/UserImage/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/UserImage/") + fileName);

                image.ImageFile.SaveAs(fileName);
                image.Title = fileName;
                image.UserId = ThisUser();


            var ctx = new Gr8DbContext();
                ctx.Images.Add(image);
                ctx.SaveChanges();
                

            return RedirectToAction("MyProfile", "Profile");
        }


        [HttpGet]
        public ActionResult LoadImage(int id) 
        {
            id = ThisUser();
            Image image = new Image();
            using (Gr8DbContext db = new Gr8DbContext())
            {
                image = db.Images.Where(i => i.Id == id).FirstOrDefault();
            }
            return View(image);
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