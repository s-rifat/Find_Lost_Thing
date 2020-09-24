using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("User Name")]
        public string userName { get; set; }
        [Required]

        [DisplayName("Password")]
        public string password { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string phoneNumber { get; set; }
    }
   
}