using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    public class UserInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserInfoes
        public ActionResult Index()
        {
            return View(db.UserInfos.ToList());
        }
        public ActionResult TEST()
        {
            return View();
        }

        public ActionResult SearchUser()
        {
            return View();
        }



        public ActionResult SearchProfile(string name)
        {
            var userId = from a in db.UserInfos
                         where a.firstName == name || a.email == name
                         select a.userId;
            int temporaryUserId = 0;
            foreach (var item in userId)
            {
                temporaryUserId = item;
            }
            List<ProfilePicture> images = GetImages();

            ProfilePicture imageFile = new ProfilePicture();
            foreach (var item in images)
            {
                if (item.userId == temporaryUserId)
                {
                    imageFile = item;
                }
            }
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(imageFile.profileImage, 0, imageFile.profileImage.Length);



            var userInfo = from b in db.UserInfos
                           where b.userId == temporaryUserId
                           select b;
            List<string> userInfos = new List<string>();

            foreach (var item in userInfo.ToList())
            {
                userInfos.Add(item.email);
                userInfos.Add(item.firstName);
                userInfos.Add(item.lastName);
                userInfos.Add(item.height.ToString());
                userInfos.Add(item.weight.ToString());
                userInfos.Add(item.age.ToString());
                userInfos.Add(item.userId.ToString());
            }
            ViewData["userInfo"] = userInfos;




            string quote = getQuote(temporaryUserId)[0].quote;
            ViewData["userQuote"] = quote;
            ViewData["userPost"] = getPost(temporaryUserId);
            ViewData["postTime"] = getPostDate(temporaryUserId);
            ViewData["userId"] = temporaryUserId;
            var buddy = TempData["buddy"] as string;
            if(buddy == "Sent")
            {
                ViewData["status"] = 2;
            }
            else
            {
                ViewData["status"] = 1;
            }

            return View();
        }


        public ActionResult CheckFriend(string name)
        {
            var id = from c in db.UserInfos
                     where c.email == User.Identity.Name
                     select c.userId;

            var userId = id.ToList()[0];
            var email = from b in db.UserInfos
                        where b.firstName == name || b.email == name
                        select b.email;

            var buddyEmail = email.ToList()[0];

            var item = from a in db.Buddies
                       where a.buddyEmail == buddyEmail && a.userId == userId
                       select a;


            if (item.Count() > 0)
            {
                var itemList = item.ToList()[0];
                if (itemList.status == "Accepted")
                {
                    return RedirectToAction("ViewUserProfile", "UserInfoes", new { name = name });
                }
                else if (itemList.status == "Sent")
                {
                    TempData["buddy"] = item.ToList()[0].status;
                    return RedirectToAction("SearchProfile", "UserInfoes", new { name = name });
                }
                else
                {
                    TempData["buddy"] = "Not Sent";
                    return RedirectToAction("SearchProfile", "UserInfoes", new { name = name });
                }
            }
            else
            {
                TempData["buddy"] = "Not Sent";
                return RedirectToAction("SearchProfile", "UserInfoes", new { name = name });
            }
        }



        public ActionResult ViewUserProfile(string name)
        {
            var userId = from a in db.UserInfos
                         where a.firstName == name || a.email == name
                         select a.userId;
            int temporaryUserId = 0;
            foreach (var item in userId)
            {
                temporaryUserId = item;
            }
            List<ProfilePicture> images = GetImages();

            ProfilePicture imageFile = new ProfilePicture();
            foreach (var item in images)
            {
                if (item.userId == temporaryUserId)
                {
                    imageFile = item;
                }
            }
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(imageFile.profileImage, 0, imageFile.profileImage.Length);

            

            var userInfo = from b in db.UserInfos
                           where b.userId == temporaryUserId
                           select b;
            List<string> userInfos = new List<string>();

            foreach (var item in userInfo.ToList())
            {
                userInfos.Add(item.email);
                userInfos.Add(item.firstName);
                userInfos.Add(item.lastName);
                userInfos.Add(item.height.ToString());
                userInfos.Add(item.weight.ToString());
                userInfos.Add(item.age.ToString());
                userInfos.Add(item.userId.ToString());
            }
            ViewData["userInfo"] = userInfos;




            string quote = getQuote(temporaryUserId)[0].quote;
            ViewData["userQuote"] = quote;
            ViewData["userPost"] = getPost(temporaryUserId);
            ViewData["postTime"] = getPostDate(temporaryUserId);
            ViewData["userId"] = temporaryUserId;

            return View();

        }


        [HttpPost]
        public ActionResult FindUser(string name)
        {
            return Json(Url.Action("CheckFriend", "UserInfoes", new { name = name }));
        }

        public ActionResult UserProfile()
        {
            var userId = from a in db.UserInfos
                         where a.email == User.Identity.Name
                         select a.userId;
            int temporaryUserId = 0;
            foreach (var item in userId)
            {
                temporaryUserId = item;
            }
            List<ProfilePicture> images = GetImages();

            ProfilePicture imageFile = new ProfilePicture();
            foreach (var item in images)
            {
                if (item.userId == temporaryUserId)
                {
                    imageFile = item;
                }
            }
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(imageFile.profileImage, 0, imageFile.profileImage.Length);
            
            ViewData["userInfo"] = GetUserInfo();
            string quote = getQuote(temporaryUserId)[0].quote;
            ViewData["userQuote"] = quote;
            ViewData["userPost"] = getPost(temporaryUserId);
            ViewData["postTime"] = getPostDate(temporaryUserId);

            return View();
        }

        public List<string> getPostDate(int userId)
        {
            var post = from a in db.UserPosts
                       where a.userId == userId
                       select a;
            List<string> postDate = new List<string>();
            foreach (var item in post.ToList())
            {
                postDate.Add(item.time.ToString());
            }
            return (postDate);
        }

        public List<string> getPost(int userId)
        {
            var post = from a in db.UserPosts
                        where a.userId == userId
                        select a;
            List<string> userPosts = new List<string>();

            foreach (var item in post.ToList())
            {
                userPosts.Add(item.message);
            }
            return (userPosts);
        }

        public List<UserQuote> getQuote(int userId)
        {

            var quote = from a in db.UserQuotes
                        where a.userId == userId
                        select a;

            return (quote.ToList());
        }



        // GET: UserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfos.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,firstName,lastName,age,height,weight,email")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfo userInfo2 = new UserInfo
                {
                    firstName = userInfo.firstName,
                    lastName = userInfo.lastName,
                    age = userInfo.age,
                    height = userInfo.height,
                    weight = userInfo.weight,
                    email = User.Identity.Name
                };
                db.UserInfos.Add(userInfo2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfos.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,firstName,lastName,age,height,weight,email")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.email = User.Identity.Name;
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfilePage","UserInfoes");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfos.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfos.Find(id);
            db.UserInfos.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddImage()
        {
            ProfilePicture profilePicture = new ProfilePicture();
            return View(profilePicture);
        }

        [HttpPost]
        public ActionResult AddImage(ProfilePicture model, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                model.profileImage = new byte[image1.ContentLength];
                image1.InputStream.Read(model.profileImage, 0, image1.ContentLength);
                
            }

            using (var dbz = new ApplicationDbContext())
            {
                var loggedInUser = from a in dbz.UserInfos
                                   where a.email == User.Identity.Name
                                   select a.userId;
                int userId = 0;
                foreach (var item in loggedInUser)
                {
                    model.userId = item;
                }
                var duplicateTest = from b in dbz.ProfilePictures
                                    where b.userId == model.userId
                                    select b;


                if (duplicateTest.ToList().Count == 0)
                {
                    dbz.ProfilePictures.Add(model);
                    dbz.SaveChanges();
                }
                else
                {
                    foreach (var item in dbz.ProfilePictures.ToList())
                    {
                        if (item.userId == model.userId)
                        {
                            item.profileImage = model.profileImage;
                            dbz.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("ProfilePage", "UserInfoes");
        }

        public ActionResult ProfilePage()
        {
            var userId = from a in db.UserInfos
                         where a.email == User.Identity.Name
                         select a.userId;
            int temporaryUserId=0;
            foreach(var item in userId)
            {
                temporaryUserId = item;
            }
            List < ProfilePicture > images = GetImages();

            ProfilePicture imageFile = new ProfilePicture();
            foreach(var item in images)
            {
                if (item.userId == temporaryUserId)
                {
                    imageFile = item;
                }
            }
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(imageFile.profileImage, 0, imageFile.profileImage.Length);

            ViewData["userInfo"] = GetUserInfo();
            return View();
        }

        public List<string> GetUserInfo()
        {
            var userInfo = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b;
            List<string> userInfos = new List<string>();
            foreach (var item in userInfo.ToList())
            {
                userInfos.Add(item.email);
                userInfos.Add(item.firstName);
                userInfos.Add(item.lastName);
                userInfos.Add(item.height.ToString());
                userInfos.Add(item.weight.ToString());
                userInfos.Add(item.age.ToString());
                userInfos.Add(item.userId.ToString());
            }
            return userInfos;
        }

        private List<ProfilePicture> GetImages()
        {
            string query = "SELECT * FROM ProfilePictures";
            List<ProfilePicture> images = new List<ProfilePicture>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            images.Add(new ProfilePicture
                            {
                                profileImage = (byte[])sdr["profileImage"],
                                userId = (int)sdr["userId"]
                            });
                        }
                    }
                    con.Close();
                }

                return images;
            }
        }
    }
}
