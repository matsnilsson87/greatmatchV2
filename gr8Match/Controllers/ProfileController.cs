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
            model.Active = true;
            ctx.Users.Add(model);
            ctx.SaveChanges();

            return Redirect(Url.Action("MyProfile", "Profile"));
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
                MyUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault(),
           
                MyInterests = ctx.Database.SqlQuery<string>("select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList(),
                MyPosts = ctx.Posts.Where(i => i.WrittenTo == id).ToList()
            };
            return View(viewModel);
        }

        public ActionResult OtherProfile(int id) {

            
            var ctx = new Gr8DbContext();
            var viewModel = new OtherProfileViewModel
            {
                OtherUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault(),
               
                OtherUserInterests = ctx.Database.SqlQuery<string>("select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList()
               
            };
            return View(viewModel);



        }
        public ActionResult MyFriends()
        {
            var id = User.Identity.GetUserId();
            var ctx = new Gr8DbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Users = ctx.Database.SqlQuery<User>("Select * From Users Where Users.Id in (Select ToUser From FriendRequests Where FromUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'True')) or Users.Id in (Select FromUser From FriendRequests Where ToUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'True'))")
                                                    .ToList(),
                FriendsRequests = ctx.Database.SqlQuery<User>("Select * From Users Where Users.Id in (Select FromUser From FriendRequests Where ToUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'False'))")
                                                    .ToList()

                                                    // denna kommentar vill man ha
            };
            return View(viewModel);
        }


        public  ActionResult SearchBar(string search)
        {
            var ctx = new Gr8DbContext();
            var lista = new SeachBarViewModel
            {
                User = ctx.Users.Where(x => x.FirstName.Contains(search) || search == null ).ToList()
            };

            return View(lista);
        }

      
        public ActionResult ConfirmFrinedRequest(int? id)
        {

            var MyId = ThisUser();
            
            var ctx = new Gr8DbContext();

           ctx.Database.ExecuteSqlCommand("Update FriendRequests Set Accepted = 'True' Where FromUser = '"+ id.ToString() + "' and ToUser = '" + MyId.ToString()+"'" );
                ctx.SaveChanges();
            
            return RedirectToAction("MyFriends", "Profile");
        }

        public ActionResult DenyFrinedRequest(int? id)
        {

            var MyId = ThisUser();

            var ctx = new Gr8DbContext();

            ctx.Database.ExecuteSqlCommand("delete from FriendRequests Where FromUser = '" + id.ToString() + "' and ToUser = '" + MyId.ToString() + "'");
            ctx.SaveChanges();

            return RedirectToAction("MyFriends", "Profile");
        }

        public ActionResult FriendRequest(int Id)
        {
            int myId = ThisUser();
            int friendId = Id;
            var ctx = new Gr8DbContext();
            int friends = ctx.Database.SqlQuery<int>("Select Count(*) From FriendRequests Where FromUser = " + myId + " and ToUser = " + friendId + " and Accepted = 'True' or ToUser = " + myId + " and FromUser = " + friendId + " and Accepted = 'True'").Sum();
            int friendRequest = ctx.Database.SqlQuery<int>("Select Count(*) From FriendRequests Where FromUser = " + myId + " and ToUser = " + friendId + " and Accepted = 'False' or ToUser = " + myId + " and FromUser = " + friendId + " and Accepted = 'False'").Sum();

            if (friends > 0)
            {
                //meddela att ni redan är vänner
                return RedirectToAction("MyFriends", "Profile");
            }

            else if (friendRequest > 0)
            {
                //meddela att det finns en vännförfrågan som inte är godkänd
                return RedirectToAction("MyFriends", "Profile");
            }

            else 
            {
                ctx.Database.ExecuteSqlCommand("Insert into FriendRequests values(" + myId + ", " + friendId + ", 'False') ");
                ctx.SaveChanges();
                return RedirectToAction("MyFriends", "Profile");
            }
         
        }


    }
}

