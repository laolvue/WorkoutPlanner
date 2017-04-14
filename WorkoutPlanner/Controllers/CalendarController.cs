using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Controllers
{
    public class CalendarController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calendar
        public ActionResult Index(int id)
        {
            var bling = from b in db.UserInfos
                        where b.userId == id
                        select b.email;
            var email = bling.ToList()[0];
            if(email != User.Identity.Name)
            {
                ViewData["isUser"] = 1;
            }
            else
            {
                ViewData["isUser"] = 2;
            }

            TempData["userId"] = id.ToString();
            return View();
        }

        public JsonResult GetEvents()
        {
            //Here MyDatabaseEntities is our entity datacontext (see Step 4)
            using (CalendarEvent dc = new CalendarEvent())
            {
                string user = TempData["userId"] as string;
                int userId = Convert.ToInt32(user);

                var bling = from b in db.UserInfos
                            where b.userId == userId
                            select b.email;

                var blin2 = bling.ToList()[0];

                var userEvents = from a in dc.Eventfulzs
                                 where a.userEmail == blin2
                                 select a;

                var v = userEvents.OrderBy(a => a.StartAt).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}