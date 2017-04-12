using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class UserQuote
    {
        [Key]
        public int Id { get; set; }
        public string quote { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}