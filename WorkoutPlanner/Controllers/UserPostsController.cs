using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    public class UserPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPosts
        public ActionResult Index()
        {
            var userPosts = db.UserPosts.Include(u => u.UserInfo);
            return View(userPosts.ToList());
        }

        // GET: UserPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = db.UserPosts.Find(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            return View(userPost);
        }

        // GET: UserPosts/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName");
            return View();
        }

        // POST: UserPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,message,time,userId")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                db.UserPosts.Add(userPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userPost.userId);
            return View(userPost);
        }

        [HttpPost]
        public ActionResult PostMessage(string message, DateTime dateSent)
        {
            var incomingMessage = message;
            var user = from a in db.UserInfos
                       where a.email == User.Identity.Name
                       select a.userId;

            UserPost userPost = new UserPost
            {
                message = message,
                time = dateSent,
                userId = user.ToList()[0]
            };
            db.UserPosts.Add(userPost);
            db.SaveChanges();
            return Json(Url.Action("UserProfile", "UserInfoes"));
        }

        // GET: UserPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = db.UserPosts.Find(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userPost.userId);
            return View(userPost);
        }

        // POST: UserPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,message,time,userId")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userPost.userId);
            return View(userPost);
        }

        // GET: UserPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userPost = db.UserPosts.Find(id);
            if (userPost == null)
            {
                return HttpNotFound();
            }
            return View(userPost);
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPost userPost = db.UserPosts.Find(id);
            db.UserPosts.Remove(userPost);
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
    }
}
