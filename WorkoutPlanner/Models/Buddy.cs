using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class Buddy
    {
        [Key]
        public int buddyId { get; set; }
        public string buddyEmail { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
        public string status { get; set; }
    }
}