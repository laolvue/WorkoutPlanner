using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class Eventful
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Workout Muscle:")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Day")]
        public DateTime StartAt { get; set; }
        [Display(Name = "End Day")]
        public DateTime EndAt { get; set; }
        public string IsFullDay { get; set; }
    }
}