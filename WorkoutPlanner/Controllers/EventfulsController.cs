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
    public class EventfulsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eventfuls
        public ActionResult Index()
        {
            return View(db.Eventfuls.ToList());
        }

        // GET: Eventfuls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventful eventful = db.Eventfuls.Find(id);
            if (eventful == null)
            {
                return HttpNotFound();
            }
            return View(eventful);
        }

        // GET: Eventfuls/Create
        public ActionResult Create()
        {
            List<SelectListItem> muscleName = new List<SelectListItem>();
            foreach( var j in db.Muscles)
            {
                string temporaryName = j.muscleName.ToString();
                string temporaryId = j.muscleId.ToString();
                muscleName.Add(new SelectListItem { Text = temporaryName, Value = temporaryId });
            }
            ViewData["muscleName"] = muscleName;
            return View();
        }

        // POST: Eventfuls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,StartAt,EndAt,IsFullDay")] Eventful workoutEvent)
        {
            if (ModelState.IsValid)
            {
                var muscleName = from a in db.Muscles
                                 where a.muscleId.ToString() == workoutEvent.Title
                                 select a.muscleName;
                var fea = muscleName.ToList();
                string joe = fea[0];


                Eventful eventful2 = new Eventful
                {
                    Title = joe,
                    Description = workoutEvent.Description,
                    StartAt = workoutEvent.StartAt,
                    EndAt = workoutEvent.EndAt,
                    IsFullDay = workoutEvent.IsFullDay
                   
                };
                db.Eventfuls.Add(eventful2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Eventfuls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventful eventful = db.Eventfuls.Find(id);
            if (eventful == null)
            {
                return HttpNotFound();
            }
            return View(eventful);
        }

        // POST: Eventfuls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,StartAt,EndAt,IsFullDay")] Eventful eventful)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventful).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventful);
        }

        // GET: Eventfuls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventful eventful = db.Eventfuls.Find(id);
            if (eventful == null)
            {
                return HttpNotFound();
            }
            return View(eventful);
        }

        // POST: Eventfuls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eventful eventful = db.Eventfuls.Find(id);
            db.Eventfuls.Remove(eventful);
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
