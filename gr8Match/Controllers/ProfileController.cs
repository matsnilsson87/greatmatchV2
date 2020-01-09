using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using gr8Match.Models;
using System.Xml.Serialization;
using System.IO;

namespace gr8Match.Controllers
{
    public class ProfileController : Controller
    {


        // GET: User
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");

            }
        }
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            try
            {
                var ctx = new Gr8DbContext();
                model.Active = true;
                ctx.Users.Add(model);
                ctx.SaveChanges();

                return Redirect(Url.Action("MyProfile", "Profile"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }

        }
        public int ThisUser()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public ActionResult MyProfile()
        {
            try
            {
                int id = ThisUser();
                var ctx = new Gr8DbContext();
                var viewModel = new MyProfileViewModel
                {
                    MyUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault(),

                    MyInterests = ctx.Database.SqlQuery<string>("Select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList(),
                    MyPosts = ctx.Posts.Where(i => i.WrittenTo == id).ToList()
                };
                return View(viewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            try
            {
                using (Gr8DbContext gr8Db = new Gr8DbContext())
                {

                    return View(gr8Db.Users.FirstOrDefault(o => o.Id == id));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
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

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult DeletePost(int id)
        {
            try
            {
                var ctx = new Gr8DbContext();
                ctx.Database.ExecuteSqlCommand("Delete from Posts where id = " + id);


                return RedirectToAction("MyProfile", "Profile");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AddInterests()
        {
            try
            {
                using (Gr8DbContext gr8Db = new Gr8DbContext())
                {

                    return View();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddInterests(FormCollection profil)
        {
            try
            {
                var MyId = ThisUser();
                var ctx = new Gr8DbContext();
                string intresse = Request["Name"];
                var lista1 = ctx.Database.SqlQuery<string>("Select Name From Interests").ToList();
                bool boolean = true;
                foreach (var namn in lista1)
                {
                    if (intresse.Equals(namn))
                    {
                        boolean = false;

                    }

                }

                if (boolean)
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult DeleteInterest()
        {
            try
            {
                using (Gr8DbContext gr8Db = new Gr8DbContext())
                {

                    return View();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteInterest(FormCollection profil)
        {
            try
            {
                var MyId = ThisUser();

                var ctx = new Gr8DbContext();
                string intresse = Request["Name"];

                ctx.Database.ExecuteSqlCommand("Delete From UserInterests Where UserId=" + MyId + " And Interest=(Select Id From Interests where Name='" + intresse + "')");
                ctx.SaveChanges();

                return RedirectToAction("MyProfile", "Profile");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }


        public ActionResult InactivateAccount()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }
        public ActionResult Deactivate()
        {
            try
            {
                int id = ThisUser();
                var ctx = new Gr8DbContext();
                ctx.Database.ExecuteSqlCommand("Update Users Set Active = 'False' Where Id =" + id);
                ctx.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult ActivateAccount()
        {
            try
            {
                int id = ThisUser();
                var ctx = new Gr8DbContext();
                ctx.Database.ExecuteSqlCommand("Update Users Set Active = 'True' Where Id =" + id);
                ctx.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult OtherProfile(int id)
        {
            try
            {
                var myId = ThisUser();
                var time = DateTime.Now;
                var ctx = new Gr8DbContext();
                var viewModel = new OtherProfileViewModel
                {
                    OtherUser = ctx.Users.Where(i => i.Id == id).FirstOrDefault(),
                    OtherUserInterests = ctx.Database.SqlQuery<string>("select Name from Interests join UserInterests on UserInterests.Interest=Interests.Id where UserInterests.UserId ='" + id.ToString() + "'").ToList()

                };
                ctx.Database.ExecuteSqlCommand("Insert into Visitors Values (" + myId + ", " + id + ", '" + time + "')");
                ctx.SaveChanges();
                return View(viewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult MatchControl(int id)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }

        }


        public ActionResult MyFriends()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult MatchSearch()
        {
            try
            {
                var MyId = ThisUser();
                var ctx = new Gr8DbContext();
                var viewmodel = new SeachBarViewModel
                {
                    User = ctx.Database.SqlQuery<User>("Select * From Users Where Id in (Select UserId From UserInterests Where Interest in (Select Interest From UserInterests Where UserId =" + MyId + ")) And Id !=" + MyId).ToList()
                };

                return View(viewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }


        public ActionResult SearchBar(string search)
        {
            try
            {
                var MyId = ThisUser();
                var ctx = new Gr8DbContext();
                var lista = new SeachBarViewModel
                {
                    User = ctx.Users.Where(x => x.FirstName.Contains(search) && x.Active == true && x.Id != MyId || search == null && x.Active == true && x.Id != MyId).ToList()
                };

                return View(lista);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }


        public ActionResult ConfirmFriendRequest(int? id)
        {
            try
            {
                var MyId = ThisUser();

                var ctx = new Gr8DbContext();

                ctx.Database.ExecuteSqlCommand("Update FriendRequests Set Accepted = 'True' Where FromUser = '" + id.ToString() + "' and ToUser = '" + MyId.ToString() + "'");
                ctx.SaveChanges();

                return RedirectToAction("MyFriends", "Profile");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult DenyFriendRequest(int? id)
        {
            try
            {
                var MyId = ThisUser();

                var ctx = new Gr8DbContext();

                ctx.Database.ExecuteSqlCommand("delete from FriendRequests Where FromUser = '" + id.ToString() + "' and ToUser = '" + MyId.ToString() + "'");
                ctx.SaveChanges();

                return RedirectToAction("MyFriends", "Profile");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }

        public ActionResult FriendRequest(int Id)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }

        }

        public static string GetCategory(int Id, string identityId) 
        {    
            int friendId = Id;
            var ctx = new Gr8DbContext();
            int myId = ctx.Database.SqlQuery<int>("Select Id from Users where identityId = '" + identityId + "'").FirstOrDefault();
            string category = ctx.Database.SqlQuery<string>("Select CategoryName From Categories join FriendInCategories on Categories.Id = FriendInCategories.CategoryId join FriendRequests on FriendRequests.Id = FriendInCategories.FriendshipId where FriendRequests.FromUser = " + friendId + " or FriendRequests.ToUser = " + friendId + " and FriendInCategories.UserId = " + myId).FirstOrDefault();

            if (category == null) 
            {
                category = "Kattegorilös och utan klös";
            }

            return category;
        }

        public ActionResult RemoveCategory(int Id) 
        {
            int friendId = Id;
            var ctx = new Gr8DbContext();


            return RedirectToAction("MyFriends", "Profile");
        }

        public ActionResult SaveAsXML()
        {
            try
            {
                var id = ThisUser();
                var ctx = new Gr8DbContext();
                List<User> userList = new List<User>();
                userList = ctx.Database.SqlQuery<User>("select * from Users where Id =" + id).ToList();



                if (userList != null)
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(List<User>));
                    TextWriter myWriter = new StreamWriter("~/Images/MyProfile.xml", true);
                    mySerializer.Serialize(myWriter, userList);
                    myWriter.Close();

                }

                return RedirectToAction("MyProfile", "Profile");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
        }
    }
}

