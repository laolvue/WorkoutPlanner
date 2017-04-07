using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkoutPlanner.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            //Here MyDatabaseEntities is our entity datacontext (see Step 4)
            using (WorkoutPlans dc = new WorkoutPlans())
            {
                var v = dc.Eventfulzs.OrderBy(a => a.StartAt).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}