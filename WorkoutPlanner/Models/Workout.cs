using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class Workout
    {
        [Key]
        public int workoutId { get; set; }
        public string workoutName { get; set; }
        [ForeignKey("Exercise")]
        public int exerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public string userEmail { get; set; }

    }
}