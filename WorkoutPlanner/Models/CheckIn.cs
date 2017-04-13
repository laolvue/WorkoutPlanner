using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class CheckIn
    {
        [Key]
        public int checkInId { get; set; }
        public int fourSquareUser { get; set; }
        public string checkInPlace { get; set; }
        public string checkInAddress { get; set; }
        public DateTime checkInTime { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}