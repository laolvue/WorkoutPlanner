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
    public class DayPlannersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DayPlanners
        public ActionResult Index()
        {
            var dayPlanners = db.DayPlanners.Include(d => d.Workout);
            return View(dayPlanners.ToList());
        }

        // GET: DayPlanners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayPlanner dayPlanner = db.DayPlanners.Find(id);
            if (dayPlanner == null)
            {
                return HttpNotFound();
            }
            return View(dayPlanner);
        }

        // GET: DayPlanners/Create
        public ActionResult Create()
        {
            ViewBag.workoutId = new SelectList(db.Workouts, "workoutId", "workoutName");
            return View();
        }

        // POST: DayPlanners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dayPlannerId,workoutId,startAt,endAt")] DayPlanner dayPlanner)
        {
            if (ModelState.IsValid)
            {
                db.DayPlanners.Add(dayPlanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.workoutId = new SelectList(db.Workouts, "workoutId", "workoutName", dayPlanner.workoutId);
            return View(dayPlanner);
        }

        // GET: DayPlanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayPlanner dayPlanner = db.DayPlanners.Find(id);
            if (dayPlanner == null)
            {
                return HttpNotFound();
            }
            ViewBag.workoutId = new SelectList(db.Workouts, "workoutId", "workoutName", dayPlanner.workoutId);
            return View(dayPlanner);
        }

        // POST: DayPlanners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dayPlannerId,workoutId,startAt,endAt")] DayPlanner dayPlanner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dayPlanner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workoutId = new SelectList(db.Workouts, "workoutId", "workoutName", dayPlanner.workoutId);
            return View(dayPlanner);
        }

        // GET: DayPlanners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayPlanner dayPlanner = db.DayPlanners.Find(id);
            if (dayPlanner == null)
            {
                return HttpNotFound();
            }
            return View(dayPlanner);
        }

        // POST: DayPlanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DayPlanner dayPlanner = db.DayPlanners.Find(id);
            db.DayPlanners.Remove(dayPlanner);
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
