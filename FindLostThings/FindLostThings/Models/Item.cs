using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Product Name")]
        public string productName { get; set; }

        [DisplayName("Manufacturer")]
        public string manufacturer { get; set; }

        [DisplayName("Model")]
        public string model { get; set; }

        [Required]

        [DisplayName("Color")]
        public string color { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Postal code must have 4 digits")]

        [DisplayName("Postal Code")]
        public int postalCode { get; set; }

        [Required]

        [DisplayName("Date")]
        public Nullable<System.DateTime> date { get; set; }


        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Item Type")]
        public string itemType { get; set; }

     
        public int userId { get; set; }

       
    }
}