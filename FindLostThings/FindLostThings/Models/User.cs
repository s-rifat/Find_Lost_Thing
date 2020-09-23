using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindLostThings.Models
{
    [MetadataType(typeof(User))]
    public partial class Account
    {
    }

    public class User
    {
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }

        [Required]
        public string phoneNumber { get; set; }
    }
   
}