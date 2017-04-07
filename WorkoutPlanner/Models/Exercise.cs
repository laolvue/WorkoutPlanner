using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class Exercise
    {
        [Key]
        public int exerciseId { get; set; }
        public string exerciseName { get; set; }
        [ForeignKey("Muscle")]
        public int muscleId { get; set; }
        public Muscle Muscle { get; set; }

    }
}