﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        
        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            using(Gr8DbContext gr8Db = new Gr8DbContext())
            {
                
                return View(gr8Db.Users.FirstOrDefault(o => o.Id == id));
                    
            }
        }
        
        [HttpPost]
        public ActionResult EditProfile(int id, FormCollection profil)
        {
            try
            {
                using (Gr8DbContext gr8Db = new Gr8DbContext())
                {
                    var result = gr8Db.Users.SingleOrDefault(o => o.Id == id);
                    result.FirstName = Request["FirstName"];
                    result.LastName = Request["LastName"];
                    
                    string dateInput = Request["DateOfBirth"];
                    DateTime parsedDate = DateTime.Parse(dateInput);
                    result.DateOfBirth = parsedDate;

                    var lista1 = gr8Db.Database.SqlQuery<string>("Select Name From Interests");
                    var lista2 = gr8Db.Database.SqlQuery<string>("Select Name From Interests Join UserInterests On Interests.Id = UserInterests.Interest Where UserId ='" + id.ToString() + "'");
                    




                    
                    

                    gr8Db.SaveChanges();
                }
                return Redirect(Url.Action("MyProfile", "Profile"));

            }

            catch {
                return View();
            }
        }

        public ActionResult DeletePost(int id) 
        {
            
                var ctx = new Gr8DbContext();
                ctx.Database.ExecuteSqlCommand("Delete from Posts where id = " + id);
            

            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpGet]
        public ActionResult AddInterests()
        {
            using (Gr8DbContext gr8Db = new Gr8DbContext())
            {

                return View();

            }
        }

        [HttpPost]
        public ActionResult AddInterests(FormCollection profil)
        {

            var MyId = ThisUser();
            var ctx = new Gr8DbContext();
            string intresse = Request["Name"];
            var lista1 = ctx.Database.SqlQuery<string>("Select Name From Interests").ToList();
            bool boolean = true;
            foreach(var namn in lista1)
            {
                if (intresse.Equals(namn))
                {
                    boolean = false;
                   
                }
                                  
            }

            if(boolean)
            {
                ctx.Database.ExecuteSqlCommand("Insert into Interests Values('" + intresse + "')");
                ctx.Database.ExecuteSqlCommand("Insert into UserInterests Values(" + MyId + ", (Select Id From Interests Where Name='" + intresse + "'))");
            }

            else
            {
                ctx.Database.ExecuteSqlCommand("Insert into UserInterests Values(" + MyId + ", (Select Id From Interests Where Name='" + intresse + "'))");
            }

            ctx.SaveChanges();

            return RedirectToAction("MyProfile", "Profile");
        }

        [HttpGet]
        public ActionResult DeleteInterest()
        {
            using (Gr8DbContext gr8Db = new Gr8DbContext())
            {

                return View();

            }
        }

        [HttpPost]
        public ActionResult DeleteInterest(FormCollection profil)
        {

            var MyId = ThisUser();

            var ctx = new Gr8DbContext();
            string intresse = Request["Name"];

            ctx.Database.ExecuteSqlCommand("Delete From UserInterests Where UserId=" + MyId + " And Interest=(Select Id From Interests where Name='" + intresse + "')");
            ctx.SaveChanges();

            return RedirectToAction("MyProfile", "Profile");
        }


        public ActionResult InactivateAccount()
        {
            return View();
        }

        public ActionResult Deactivate()
        {
            int id = ThisUser();
            var ctx = new Gr8DbContext();
            ctx.Database.ExecuteSqlCommand("Update Users Set Active = 'False' Where Id =" + id);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ActivateAccount()
        {
            int id = ThisUser();
            var ctx = new Gr8DbContext();
            ctx.Database.ExecuteSqlCommand("Update Users Set Active = 'True' Where Id =" + id);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OtherProfile(int id) {

            var myId = ThisUser();
            var time = DateTime.Now;
            var ctx = new Gr8DbContext();
            var viewModel = new OtherProfileViewModel
            {
                OtherUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault(),
                OtherUserInterests = ctx.Database.SqlQuery<string>("select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList()
               
            };
            ctx.Database.ExecuteSqlCommand("Insert into Visitors Values ("+ myId +", "+ id +", '"+ time +"')");
            ctx.SaveChanges();
            return View(viewModel);
        }
        
        public ActionResult MatchControl(int id)
        {
            var myId = ThisUser();           
            var ctx = new Gr8DbContext();

            var viewmodel = new MatchControlViewModel
            {
                Interests = ctx.Database.SqlQuery<string>("Select Name From Interests Where Id in (Select Interest From UserInterests Where UserId=" + myId + ") And Id in (Select Interest From UserInterests Where UserId=" + id + ")").ToList(),
                OtherUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault()
            };   
            
            return View(viewmodel);

        }

      
        public ActionResult MyFriends()
        {
            var id = User.Identity.GetUserId();
            var ctx = new Gr8DbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Users = ctx.Database.SqlQuery<User>("Select * From Users Where Users.Id in (Select ToUser From FriendRequests Where FromUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'True')) or Users.Id in (Select FromUser From FriendRequests Where ToUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'True')) and Active = 'True'")
                                                    .ToList(),
                FriendsRequests = ctx.Database.SqlQuery<User>("Select * From Users Where Users.Id in (Select FromUser From FriendRequests Where ToUser in (Select Id From Users Where IdentityID = '" + id + "' and Accepted = 'False')) and Active = 'True'")
                                                    .ToList()

                                                    // denna kommentar vill man ha
            };
            return View(viewModel);
        }

        public ActionResult MatchSearch()
        {
            
            var MyId = ThisUser();
            var ctx = new Gr8DbContext();
            var viewmodel = new SeachBarViewModel
            {
                User = ctx.Database.SqlQuery<User>("Select * From Users Where Id in (Select UserId From UserInterests Where Interest in (Select Interest From UserInterests Where UserId =" +  MyId + ")) And Id !=" +  MyId).ToList()
            };

            return View(viewmodel);
        }


        public  ActionResult SearchBar(string search)
        {
            var MyId = ThisUser();
            var ctx = new Gr8DbContext();
            var lista = new SeachBarViewModel
            {
                User = ctx.Users.Where(x => x.FirstName.Contains(search) && x.Active==true && x.Id != MyId  || search == null && x.Active == true && x.Id != MyId).ToList()
            };

            return View(lista);
        }

      
        public ActionResult ConfirmFriendRequest(int? id)
        {

            var MyId = ThisUser();
            
            var ctx = new Gr8DbContext();

           ctx.Database.ExecuteSqlCommand("Update FriendRequests Set Accepted = 'True' Where FromUser = '"+ id.ToString() + "' and ToUser = '" + MyId.ToString()+"'" );
                ctx.SaveChanges();
            
            return RedirectToAction("MyFriends", "Profile");
        }

        public ActionResult DenyFriendRequest(int? id)
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

