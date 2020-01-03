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
    public class ImageController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {
            return View();
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


        [HttpPost]
        public ActionResult Add(Image image)

        {
            int thisId = ThisUser();
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string fileExt = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmdd") + fileExt;
                image.ImgPath = "~/Images/UserImages/" + fileName;
                image.Title = fileName;
                image.UserId = thisId;
            fileName = Path.Combine(Server.MapPath("~/Images/UserImages/"), fileName);

                image.ImageFile.SaveAs(fileName);
               


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

        public ActionResult ViewFriendProfile() {



            return View();
        }
       


    }
}