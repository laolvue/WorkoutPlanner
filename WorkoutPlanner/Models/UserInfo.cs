using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class UserInfo
    {
        [Key]
        public int userId { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Age")]
        public int age { get; set; }
        [Display(Name = "Height")]
        public string height { get; set; }
        [Display(Name = "Weight")]
        public string weight { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }
}