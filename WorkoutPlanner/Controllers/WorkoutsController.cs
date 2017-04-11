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
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workouts
        public ActionResult Index()
        {
            var workouts = db.Workouts.Include(w => w.Exercise);
            return View(workouts.ToList());
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            List<SelectListItem> dayName = new List<SelectListItem>();
            dayName.Add(new SelectListItem { Text = "Mondays", Value = "1" });
            dayName.Add(new SelectListItem { Text = "Tuesdays", Value = "2" });
            dayName.Add(new SelectListItem { Text = "Wednesdays", Value = "3" });
            dayName.Add(new SelectListItem { Text = "Thursdays", Value = "4" });
            dayName.Add(new SelectListItem { Text = "Fridays", Value = "5" });
            dayName.Add(new SelectListItem { Text = "Saturday", Value = "6" });
            dayName.Add(new SelectListItem { Text = "Sunday", Value = "7" });

            ViewData["dayName"] = dayName;

            ViewBag.exerciseID = new SelectList(db.Exercises, "exerciseId", "exerciseName");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "workoutId,workoutName,notes,exerciseID,set,rep,userEmail,day,workoutImage")] Workout workout, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                workout.workoutImage = new byte[image1.ContentLength];
                image1.InputStream.Read(workout.workoutImage, 0, image1.ContentLength);
            }

            if (ModelState.IsValid)
            {
                workout.userEmail = User.Identity.Name;
                db.Workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.exerciseID = new SelectList(db.Exercises, "exerciseId", "exerciseName", workout.exerciseID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.exerciseID = new SelectList(db.Exercises, "exerciseId", "exerciseName", workout.exerciseID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "workoutId,workoutName,notes,exerciseID,set,rep,userEmail,day,workoutImage")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.exerciseID = new SelectList(db.Exercises, "exerciseId", "exerciseName", workout.exerciseID);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            db.Workouts.Remove(workout);
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
