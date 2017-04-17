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
        public string buddyOne { get; set; }
        public string buddyTwo { get; set; }
        public string message { get; set; }
        public DateTime timeSent { get; set; }
        public string channel { get; set; }
    }
}