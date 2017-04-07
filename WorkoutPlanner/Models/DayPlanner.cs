using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class DayPlanner
    {
        [Key]
        public int dayPlannerId { get; set; }
        [ForeignKey("Workout")]
        public int workoutId { get; set; }
        public Workout Workout { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
    }
}