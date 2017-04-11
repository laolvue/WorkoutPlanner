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
        public string notes { get; set; }
        [ForeignKey("Exercise")]
        public int exerciseID { get; set; }
        public Exercise Exercise { get; set; }
        public int set { get; set; }
        public int rep { get; set; }
        public string userEmail { get; set; }
        public string day { get; set; }
        public byte[] workoutImage { get; set; }
    }
}