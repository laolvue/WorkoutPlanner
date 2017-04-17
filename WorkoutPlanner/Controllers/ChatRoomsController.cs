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
    public class ChatRoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChatRooms
        public ActionResult Index()
        {
            var chatRooms = db.ChatRooms.Include(c => c.UserInfo);
            return View(chatRooms.ToList());
        }

        // GET: ChatRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatRoom chatRoom = db.ChatRooms.Find(id);
            if (chatRoom == null)
            {
                return HttpNotFound();
            }
            return View(chatRoom);
        }

        // GET: ChatRooms/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName");
            return View();
        }

        // POST: ChatRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,chatroom,userId")] ChatRoom chatRoom)
        {
            if (ModelState.IsValid)
            {
                db.ChatRooms.Add(chatRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", chatRoom.userId);
            return View(chatRoom);
        }

        // GET: ChatRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatRoom chatRoom = db.ChatRooms.Find(id);
            if (chatRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", chatRoom.userId);
            return View(chatRoom);
        }

        // POST: ChatRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,chatroom,userId")] ChatRoom chatRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.UserInfos, "userId", "firstName", chatRoom.userId);
            return View(chatRoom);
        }

        // GET: ChatRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatRoom chatRoom = db.ChatRooms.Find(id);
            if (chatRoom == null)
            {
                return HttpNotFound();
            }
            return View(chatRoom);
        }

        // POST: ChatRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChatRoom chatRoom = db.ChatRooms.Find(id);
            db.ChatRooms.Remove(chatRoom);
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

        public ActionResult JoinChatRoom()
        {
            var firstName = from a in db.UserInfos
                            where a.email == User.Identity.Name
                            select a.firstName;
            var lastName = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b.lastName;
            var userName = firstName.ToList()[0] + " " + lastName.ToList()[0];

            ViewData["userName"] = userName;
            return View();
        }


        [HttpPost]
        public ActionResult GetUserName()
        {
            var firstName = from a in db.UserInfos
                            where a.email == User.Identity.Name
                            select a.firstName;
            var lastName = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b.lastName;
            var userName = firstName.ToList()[0] + " " + lastName.ToList()[0];
            return Json(userName, JsonRequestBehavior.AllowGet);
        }
    }
}
