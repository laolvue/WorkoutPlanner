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
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //get images
        private List<Workout> GetImages()
        {
            string query = "SELECT * FROM Workouts";
            List<Workout> images = new List<Workout>();
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
                            images.Add(new Workout
                            {
                                workoutImage = (byte[])sdr["workoutImage"],
                                workoutName = (string)sdr["workoutName"],
                                notes = (string)sdr["notes"],
                                set = (int)sdr["set"],
                                rep = (int)sdr["rep"],
                                exerciseID = (int)sdr["exerciseID"],
                                userEmail = (string)sdr["userEmail"],
                                day = (string)sdr["day"]
                            });
                        }
                    }
                    con.Close();
                }
                return images;
            }
        }

        public DayOfWeek getDay(Workout workout)
        {
            var dayOfWeek = DayOfWeek.Monday;
            switch (workout.day)
            {
                case "1":
                    return dayOfWeek = DayOfWeek.Monday;
                case "2":
                    return dayOfWeek = DayOfWeek.Tuesday;
                case "3":
                    return dayOfWeek = DayOfWeek.Wednesday;
                case "4":
                    return dayOfWeek = DayOfWeek.Thursday;
                case "5":
                    return dayOfWeek = DayOfWeek.Friday;
                case "6":
                    return dayOfWeek = DayOfWeek.Saturday;
                case "7":
                    return dayOfWeek = DayOfWeek.Sunday;
            }

            return dayOfWeek;
        }


        // GET: Workouts
        public ActionResult Index(int? id, int? userId)
        {
            var dayChose = DayOfWeek.Monday;
            switch (id)
            {
                case null: dayChose= DateTime.Now.DayOfWeek;
                    break;
                case 1: dayChose = DateTime.Now.DayOfWeek;
                    break;
                case 2: dayChose = DayOfWeek.Monday;
                    break;
                case 3: dayChose = DayOfWeek.Tuesday;
                    break;
                case 4: dayChose = DayOfWeek.Wednesday;
                    break;
                case 5: dayChose = DayOfWeek.Thursday;
                    break;
                case 6: dayChose = DayOfWeek.Friday;
                    break;
                case 7: dayChose = DayOfWeek.Saturday;
                    break;
                case 8: dayChose = DayOfWeek.Sunday;
                    break;
                
            }
            List<string> workoutImages = new List<string>();
            List<Workout> images = GetImages();
            IEnumerable<Workout> workout;
            if(userId != null)
            {
                var user = from c in db.UserInfos
                           where c.userId == userId
                           select c.email;

                var userEmail = user.ToList()[0];
                workout = from a in images
                              where a.userEmail == userEmail
                              select a;

                
            }
            else
            {
                workout = from a in images
                              where a.userEmail == User.Identity.Name
                              select a;

                var currentId = from g in db.UserInfos
                                where g.email == User.Identity.Name
                                select g.userId;

                userId = currentId.ToList()[0];
            }
            
            var dayOfWeek = DayOfWeek.Monday;
            List<Workout> workouts = new List<Workout>();

            foreach(var item in workout.ToList())
            {
                dayOfWeek = getDay(item);

                if(dayOfWeek == dayChose)
                {
                    workouts.Add(item);
                    var image = "data:image/png;base64," + Convert.ToBase64String(item.workoutImage, 0, item.workoutImage.Length);
                    workoutImages.Add(image);
                }
            }
            ViewData["workoutImages"] = workoutImages;
            ViewData["userId"] = userId;
            ViewBag.Day = $"{dayChose}";
            return View(workouts);
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
            var exercise = from a in db.Exercises
                                       where a.userEmail == User.Identity.Name
                                       select a;

            ViewBag.exerciseID = new SelectList(exercise, "exerciseId", "exerciseName");
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
                var exerciseName = from a in db.Exercises
                                   where a.exerciseId == workout.exerciseID
                                   select a.exerciseName;
                var exercise = "";
                foreach(var item in exerciseName.ToList())
                {
                    exercise = item;
                }
                workout.workoutName = exercise;
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
