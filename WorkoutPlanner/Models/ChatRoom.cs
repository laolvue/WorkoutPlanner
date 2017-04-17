using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class ChatRoom
    {
        [Key]
        public int id { get; set; }
        public int chatroom { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}