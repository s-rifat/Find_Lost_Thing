using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindLostThings.Models
{
    [MetadataType(typeof(Item))]

    public partial class Product
    {
    }
    public class Item
    {
        public int productId { get; set; }

        [Required]
        public string productName { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }

        [Required]
        public string color { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Postal code must have 4 digits")]
        public int postalCode { get; set; }

        [Required]
        public Nullable<System.DateTime> date { get; set; }
        public string description { get; set; }

        [Required]
        public string userType { get; set; }
        public int userId { get; set; }

       
    }
}