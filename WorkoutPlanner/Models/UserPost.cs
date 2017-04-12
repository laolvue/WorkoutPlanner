using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class UserPost
    {
        [Key]
        public int Id { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}