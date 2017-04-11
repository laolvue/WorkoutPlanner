using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class Muscle
    {
        [Key]
        public int muscleId { get; set; }
        public string muscleName { get; set; }
        public string userEmail { get; set; }

    }
}