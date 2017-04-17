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
    public class BuddiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



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


        public ActionResult ViewBuddyList()
        {
            var userId = from a in db.UserInfos
                         where a.email == User.Identity.Name
                         select a.userId;
            int temporaryUserId = 0;

            foreach (var item in userId)
            {
                temporaryUserId = item;
            }

            var buddys = from b in db.Buddies
                         where b.buddyEmail == User.Identity.Name && b.status == "Sent"
                         select b;

            List<ProfilePicture> images = GetImages();
            List<string> userImages = new List<string>();
            List<string> userNames = new List<string>();
            List<string> userStatuses = new List<string>();


            foreach (var item in buddys.ToList())
            {
                var userInfo= from e in db.UserInfos
                                where e.userId == item.userId
                                select e;

                userNames.Add(userInfo.ToList()[0].email);
                userStatuses.Add("accept or knee");

                var userIds = userInfo.ToList()[0].userId;

                foreach (var image in images)
                {
                    if (image.userId == userIds)
                    {
                        userImages.Add("data:image/png;base64," + Convert.ToBase64String(image.profileImage, 0, image.profileImage.Length));
                    }
                }

            }
            ViewData["image"] = userImages;
            ViewData["userNames"] = userNames;
            ViewData["statuses"] = userStatuses;


            var buddies = db.Buddies.Include(b => b.UserInfo);
            return View(buddies.ToList());
        }



        // GET: Buddies
        public ActionResult Index(int id)
        {
            var userId = from a in db.UserInfos
                         where a.email == User.Identity.Name
                         select a.userId;
            int temporaryUserId = 0;

            foreach (var item in userId)
            {
                temporaryUserId = item;
            }

            IQueryable<Buddy> buddys;

            if (id == 1)
            {
                buddys = from b in db.Buddies
                             where b.userId == temporaryUserId && b.status == "Sent"
                             select b;
            }
            else
            {
                buddys = from b in db.Buddies
                             where b.userId == temporaryUserId && b.status == "Accepted"
                             select b;
            }
            

            List<ProfilePicture> images = GetImages();
            List<string> userImages = new List<string>();
            List<string> userNames = new List<string>();
            List<string> userStatuses = new List<string>();
            List<string> channel = new List<string>();

            foreach (var item in buddys.ToList())
            {
                userNames.Add(item.buddyEmail);
                userStatuses.Add(item.status);

                var chatLog = from y in db.ChatRooms
                              where y.buddyOne == item.buddyEmail || y.buddyTwo == item.buddyEmail && y.buddyOne == User.Identity.Name || y.buddyTwo == User.Identity.Name
                              select y.channel;
                if (chatLog.Count() > 0)
                {
                    channel.Add(chatLog.ToList()[0]);

                }
                


                var userIds = from c in db.UserInfos
                         where item.buddyEmail == c.email
                         select c.userId;
                foreach (var image in images)
                {
                    if (image.userId == userIds.ToList()[0])
                    {
                        userImages.Add("data:image/png;base64," + Convert.ToBase64String(image.profileImage, 0, image.profileImage.Length));
                    }
                }

            }

            

            ViewData["image"] = userImages;
            ViewData["userNames"] = userNames;
            ViewData["statuses"] = userStatuses;
            ViewData["userEmail"] = User.Identity.Name;
            ViewData["channel"] = channel;
            



            var buddies = db.Buddies.Include(b => b.UserInfo);
            return View(buddies.ToList());
        }

        // GET: Buddies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buddy buddy = db.Buddies.Find(id);
            if (buddy == null)
            {
                return HttpNotFound();
            }
            return View(buddy);
        }

        // GET: Buddies/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName");
            return View();
        }

        // POST: Buddies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "buddyId,buddyEmail,userId,status")] Buddy buddy)
        {
            if (ModelState.IsValid)
            {
                db.Buddies.Add(buddy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", buddy.userId);
            return View(buddy);
        }

        // GET: Buddies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buddy buddy = db.Buddies.Find(id);
            if (buddy == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", buddy.userId);
            return View(buddy);
        }

        // POST: Buddies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "buddyId,buddyEmail,userId,status")] Buddy buddy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buddy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", buddy.userId);
            return View(buddy);
        }

        // GET: Buddies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buddy buddy = db.Buddies.Find(id);
            if (buddy == null)
            {
                return HttpNotFound();
            }
            return View(buddy);
        }

        // POST: Buddies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buddy buddy = db.Buddies.Find(id);
            db.Buddies.Remove(buddy);
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

        public ActionResult ViewBuddies()
        {
            return View();
        }

        public ActionResult AcceptBuddy(string buddyEmail)
        {
            var user = from b in db.UserInfos
                       where b.email == buddyEmail
                       select b.userId;
            

            var userId = user.ToList()[0];

            var buddy = from a in db.Buddies
                        where a.buddyEmail == User.Identity.Name && a.userId == userId
                        select a;

            var activeUser = from d in db.UserInfos
                             where d.email == User.Identity.Name
                             select d.userId;

            var activeUserId = activeUser.ToList()[0];

            buddy.ToList()[0].status = "Accepted";
            db.SaveChanges();


            var duplicate = from c in db.Buddies
                            where c.buddyEmail == buddyEmail && c.userId == activeUserId
                            select c;

            if(duplicate.ToList().Count == 0)
            {
                Buddy buddys = new Buddy
                {
                    userId = activeUserId,
                    buddyEmail = buddyEmail,
                    status = "Accepted"

                };

                db.Buddies.Add(buddys);


                var chatroomChannel = from z in db.ChatRooms
                                      where z.buddyOne == buddyEmail || z.buddyTwo == buddyEmail && z.buddyOne == User.Identity.Name || z.buddyTwo == User.Identity.Name
                                      select z;
                if (chatroomChannel.Count() == 0)
                {
                    var random = new Random();
                    ChatRoom chatroom = new ChatRoom
                    {
                        buddyOne = buddyEmail,
                        buddyTwo = User.Identity.Name,
                        message = "Chat Channel Created",
                        timeSent = DateTime.Now,
                        channel = random.Next(999999).ToString()
                    };
                    db.ChatRooms.Add(chatroom);
                }
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Buddies", new { id = 2 });
        }


        public ActionResult DeclineBuddy(string buddyEmail)
        {

            var user = from b in db.UserInfos
                        where b.email == User.Identity.Name
                        select b.userId;
            var userId = user.ToList()[0];


            var buddy = from b in db.Buddies
                        where b.buddyEmail == buddyEmail && b.userId == userId
                        select b;

            var buddyToDecline = buddy.ToList()[0];
            db.Buddies.Remove(buddyToDecline);
            db.SaveChanges();

            return RedirectToAction("ViewBuddyList", "Buddies");
        }

        public ActionResult DeleteBuddy(string buddyEmail)
        {
            var user = from a in db.UserInfos
                       where a.email == buddyEmail
                       select a.userId;

            var userId = user.ToList()[0];

            var currentUser = from b in db.UserInfos
                              where b.email == User.Identity.Name
                              select b.userId;

            var currentUserId = currentUser.ToList()[0];

            var buddy = from c in db.Buddies
                        where c.buddyEmail == buddyEmail && c.userId == currentUserId
                        select c;
            var buddy1 = buddy.ToList()[0];

            var buddy2 = from c in db.Buddies
                        where c.buddyEmail == User.Identity.Name && c.userId == userId
                        select c;
            var buddy2s = buddy2.ToList()[0];

            db.Buddies.Remove(buddy1);
            db.Buddies.Remove(buddy2s);
            db.SaveChanges();

            return RedirectToAction("index", "Buddies", new { id = 2 });
        }


        public ActionResult RequestBuddy(int id)
        {
            var buddy = from a in db.UserInfos
                        where a.userId == id
                        select a.email;
            var buddyEmails = buddy.ToList()[0];

            var user = from b in db.UserInfos
                       where b.email == User.Identity.Name
                       select b.userId;
            var userIds = user.ToList()[0];

            Buddy buddies = new Buddy
            {
                buddyEmail = buddyEmails,
                userId = userIds,
                status = "Sent"
            };

            db.Buddies.Add(buddies);
            db.SaveChanges();

            return RedirectToAction("Index", "Buddies", new { id = 1 });
        }
    }
}