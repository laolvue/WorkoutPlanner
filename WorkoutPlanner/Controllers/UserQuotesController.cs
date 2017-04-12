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
    public class UserQuotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserQuotes
        public ActionResult Index()
        {
            var userQuotes = db.UserQuotes.Include(u => u.UserInfo);
            return View(userQuotes.ToList());
        }

        // GET: UserQuotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuote userQuote = db.UserQuotes.Find(id);
            if (userQuote == null)
            {
                return HttpNotFound();
            }
            return View(userQuote);
        }

        // GET: UserQuotes/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName");
            return View();
        }

        // POST: UserQuotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,quote,userId")] UserQuote userQuote)
        {
            if (ModelState.IsValid)
            {
                var idOfUser = from a in db.UserInfos
                               where a.email == User.Identity.Name
                               select a.userId;
                var userId = idOfUser.ToList()[0];

                var quoteDuplicate = from b in db.UserQuotes
                                     where b.userId == userId
                                     select b;


                if (quoteDuplicate.Count() > 0)
                {
                    var quoteDuplicateAsList = quoteDuplicate.ToList();
                    quoteDuplicateAsList[0].quote = userQuote.quote;
                    db.SaveChanges();
                }
                else
                {
                    userQuote.userId = userId;
                    
                    db.UserQuotes.Add(userQuote);
                    db.SaveChanges();
                }

                
                return RedirectToAction("UserProfile","UserInfoes");
            }

            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userQuote.userId);
            return View(userQuote);
        }

        // GET: UserQuotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuote userQuote = db.UserQuotes.Find(id);
            if (userQuote == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userQuote.userId);
            return View(userQuote);
        }

        // POST: UserQuotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,quote,userId")] UserQuote userQuote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userQuote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", userQuote.userId);
            return View(userQuote);
        }

        // GET: UserQuotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuote userQuote = db.UserQuotes.Find(id);
            if (userQuote == null)
            {
                return HttpNotFound();
            }
            return View(userQuote);
        }

        // POST: UserQuotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserQuote userQuote = db.UserQuotes.Find(id);
            db.UserQuotes.Remove(userQuote);
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
