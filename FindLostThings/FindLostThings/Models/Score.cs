//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FindLostThings.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score
    {
        public int scoreId { get; set; }
        public int lostItemId { get; set; }
        public int foundItemId { get; set; }
        public int isVerified { get; set; }
        public int scoreManufacturer { get; set; }
        public int scoreModel { get; set; }
        public int scoreDescription { get; set; }
        public string lostUserName { get; set; }
        public string foundUserName { get; set; }
    }
}
