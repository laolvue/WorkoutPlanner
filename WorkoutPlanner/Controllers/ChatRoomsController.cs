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

        public ActionResult JoinChatRoom(string email, string userEmail)
        {
            List<string> users = new List<string>();
            var firstName = from a in db.UserInfos
                            where a.email == User.Identity.Name
                            select a.firstName;
            var lastName = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b.lastName;
            var userName = firstName.ToList()[0] + " " + lastName.ToList()[0];

            var firstName2 = from a in db.UserInfos
                             where a.email == email
                             select a.firstName;
            var lastName2 = from b in db.UserInfos
                            where b.email == email
                            select b.lastName;
            var userName2 = firstName2.ToList()[0] + " " + lastName2.ToList()[0];

            users.Add(userName);
            users.Add(userName2);

            ViewData["users"] = users;
            TempData["userEmails"] = email;


            return View();
        }


        [HttpPost]
        public ActionResult GetUserName()
        {
            var userEmail = TempData["userEmails"] as string;

            var firstName = from a in db.UserInfos
                            where a.email == User.Identity.Name
                            select a.firstName;
            var lastName = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b.lastName;
            var userName = firstName.ToList()[0] + " " + lastName.ToList()[0];

            TempData["Emails"] = userEmail;

            return Json(userName, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult LogChat(string email)
        {
            var userEmail = TempData["Emails"] as string;

            //var chatLog = from a in db.ChatRooms
            //              where a.buddyOne == userEmail || a.buddyTwo == userEmail && a.buddyOne == User.Identity.Name || a.buddyTwo == User.Identity.Name
            //              select a;

            //var chatlogs = chatLog.ToList();

            //if(chatlogs.Count() == 0)
            //{
            ChatRoom chatroom = new ChatRoom
            {
                buddyOne = userEmail,
                buddyTwo = User.Identity.Name,
                message = email,
                timeSent = DateTime.Now
            };

            db.ChatRooms.Add(chatroom);
            db.SaveChanges();
            //}

            return Json(userEmail, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetChatLog(string email)
        {
            var chatLog = from a in db.ChatRooms
                          where a.buddyOne == email || a.buddyTwo == email && a.buddyOne == User.Identity.Name || a.buddyTwo == User.Identity.Name
                          select a;

            var chatlogs = chatLog.ToList();

            List<string> chat = new List<string>();
            List<string> time = new List<string>();


            List<string> names = new List<string>();
            foreach (var item in chatlogs)
            {
                chat.Add(item.message);
                time.Add("sent at: " + item.timeSent);
            }

            ViewData["chat"] = chat;
            ViewData["time"] = time;

            var firstName = from a in db.UserInfos
                            where a.email == User.Identity.Name
                            select a.firstName;
            var lastName = from b in db.UserInfos
                           where b.email == User.Identity.Name
                           select b.lastName;
            var userName = firstName.ToList()[0] + " " + lastName.ToList()[0];

            var firstName2 = from a in db.UserInfos
                             where a.email == email
                             select a.firstName;
            var lastName2 = from b in db.UserInfos
                            where b.email == email
                            select b.lastName;
            var userName2 = firstName2.ToList()[0] + " " + lastName2.ToList()[0];
            names.Add(userName);
            names.Add(userName2);

            ViewData["names"] = names;

            return View();
        }
    }
}
