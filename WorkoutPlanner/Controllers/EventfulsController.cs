using System;
using System.Collections.Generic;
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

        [HttpPost]
        public ActionResult GetCheckIn(string userIds, DateTime checkInDate, string locationName, string locationAddress)
        {
            var user = from a in db.UserInfos
                       where a.email == User.Identity.Name
                       select a.userId;

            CheckIn checkIn = new CheckIn
            {
                fourSquareUser = userIds,
                checkInPlace = locationName,
                checkInAddress = locationAddress,
                checkInTime = checkInDate,
                userId = user.ToList()[0]
            };

            db.CheckIns.Add(checkIn);
            db.SaveChanges();

            string messageToBeSent = "Checked into" + locationName + " at: " + locationAddress;

            return Json(Url.Action("PostMessage","UserPosts"));
        }

        // GET: Eventfuls/Create
        public ActionResult Create()
        {
            List<SelectListItem> muscleName = new List<SelectListItem>();
            List<SelectListItem> dayName = new List<SelectListItem>();
            foreach ( var j in db.Muscles)
            {
                string temporaryName = j.muscleName.ToString();
                string temporaryId = j.muscleId.ToString();
                muscleName.Add(new SelectListItem { Text = temporaryName, Value = temporaryId });
            }
            ViewData["muscleName"] = muscleName;
            
            dayName.Add(new SelectListItem { Text = "Mondays", Value = "1" });
            dayName.Add(new SelectListItem { Text = "Tuesdays", Value = "2" });
            dayName.Add(new SelectListItem { Text = "Wednesdays", Value = "3" });
            dayName.Add(new SelectListItem { Text = "Thursdays", Value = "4" });
            dayName.Add(new SelectListItem { Text = "Fridays", Value = "5" });
            dayName.Add(new SelectListItem { Text = "Saturday", Value = "6" });
            dayName.Add(new SelectListItem { Text = "Sunday", Value = "7" });

            ViewData["dayName"] = dayName;

            return View();
        }

        public System.DayOfWeek getDay(Eventful workoutEvent)
        {
            System.DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (workoutEvent.Description)
            {

                case "1":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "2":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "3":
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case "4":
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case "5":
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case "6":
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case "7":
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
            }

            return (dayOfWeek);
        }

        public string getMuscleName(Eventful workoutEvent)
        {
            using (var cta = new ApplicationDbContext())
            {
                var muscleName = from a in cta.Muscles
                                 where a.muscleId.ToString() == workoutEvent.Title
                                 select a.muscleName;

                var fea = muscleName.ToList();
                string muscle = fea[0];
                return muscle;
            }
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
                var dayOfWeek = getDay(workoutEvent);
                string muscle = getMuscleName(workoutEvent);
                
                for (int i = DateTime.UtcNow.Month; i < 12; i++)
                {
                    DateTime mondayz = new DateTime(2017, i, 1);
                    while (mondayz.Day <= DateTime.DaysInMonth(2017, i))
                    {
                        while (mondayz.DayOfWeek != dayOfWeek && mondayz.Day < DateTime.DaysInMonth(2017, i))
                        {
                            mondayz = mondayz.AddDays(1);
                        }
                        if (mondayz.DayOfWeek == dayOfWeek && mondayz.Day <= DateTime.DaysInMonth(2017, i))
                        {
                            using (var ct = new ApplicationDbContext())
                            {
                                var duplicateTest = from b in ct.Eventfuls
                                                    where b.Title == muscle && b.StartAt == mondayz.Date
                                                    select b;
                                int counter = duplicateTest.ToList().Count;
                                if (counter == 0)
                                {
                                    Eventful eventful3 = new Eventful
                                    {
                                        Title = muscle,
                                        Description = "",
                                        StartAt = mondayz.Date,
                                        EndAt = mondayz.Date,
                                        IsFullDay = workoutEvent.IsFullDay,
                                        userEmail = User.Identity.Name
                                    };
                                    ct.Eventfuls.Add(eventful3);
                                    ct.SaveChanges();
                                    
                                }
                            }
                            
                                
                            if (mondayz.Day < DateTime.DaysInMonth(2017, i))
                            {
                                mondayz = mondayz.AddDays(1);
                            }
                        }
                        if (mondayz.Day == DateTime.DaysInMonth(2017, i))
                        {
                            break;
                        }

                    }

                }
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
        public ActionResult Delete()
        {
            List<SelectListItem> muscleName = new List<SelectListItem>();
            List<SelectListItem> dayName = new List<SelectListItem>();
            foreach (var j in db.Muscles)
            {
                string temporaryName = j.muscleName.ToString();
                string temporaryId = j.muscleId.ToString();
                muscleName.Add(new SelectListItem { Text = temporaryName, Value = temporaryId });
            }
            ViewData["muscleName"] = muscleName;

            dayName.Add(new SelectListItem { Text = "Mondays", Value = "1" });
            dayName.Add(new SelectListItem { Text = "Tuesdays", Value = "2" });
            dayName.Add(new SelectListItem { Text = "Wednesdays", Value = "3" });
            dayName.Add(new SelectListItem { Text = "Thursdays", Value = "4" });
            dayName.Add(new SelectListItem { Text = "Fridays", Value = "5" });
            dayName.Add(new SelectListItem { Text = "Saturday", Value = "6" });
            dayName.Add(new SelectListItem { Text = "Sunday", Value = "7" });

            ViewData["dayName"] = dayName;

            return View();
        }

        // POST: Eventfuls/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,Title,Description,StartAt,EndAt,IsFullDay")] Eventful workoutEvent)
        {
            if (ModelState.IsValid)
            {
                var dayOfWeek = getDay(workoutEvent);
                string muscle = getMuscleName(workoutEvent);
                
                using (var ctx = new ApplicationDbContext())
                {
                    var eventToDelete = from a in ctx.Eventfuls
                                        where a.Title == muscle
                                        select a;
                    foreach (var item in eventToDelete.ToList())
                    {
                        if (item.StartAt.DayOfWeek == dayOfWeek)
                        {
                            ctx.Eventfuls.Remove(item);
                            ctx.SaveChanges();
                        }
                    }
                }
                
            }

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


        public ActionResult ViewMap()
        {
            return View();
        }
    }
}
