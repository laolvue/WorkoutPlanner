using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkoutPlanner.Models
{
    public class ProfilePicture
    {
        [Key]
        public int profilePictureId { get; set; }
        public byte[] profileImage { get; set; }
        [ForeignKey("UserInfo")]
        public int userId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}