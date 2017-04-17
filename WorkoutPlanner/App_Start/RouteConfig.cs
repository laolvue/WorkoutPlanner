using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkoutPlanner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserPost",
                url: "UserPosts/PostMessage/message/dataSent",
                defaults: new { controller = "UserPosts", action = "PostMessage", message = UrlParameter.Optional, dataSent = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetCheckIn",
                url: "Eventfuls/GetCheckIn/userId/checkInDate/locationName/locationAddress",
                defaults: new { controller = "Eventfuls", action = "GetCheckIn", userId = UrlParameter.Optional, checkInDate = UrlParameter.Optional, locationName = UrlParameter.Optional, locationAddress = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "DayWorkout",
                url: "Workouts/Index/id/userId",
                defaults: new { controller = "Workouts", action = "Index", id = UrlParameter.Optional, userId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "JoinChatRoom",
                url: "{controller}/{action}/{email}",
                defaults: new { controller = "Chatrooms", action = "JoinChatRooms", email = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "JoinChatRoom2",
                url: "{controller}/{action}/{email}/{userEmail}",
                defaults: new { controller = "Chatrooms", action = "JoinChatRooms", email = UrlParameter.Optional, userEmail = UrlParameter.Optional }
            );

        }
    }
}
