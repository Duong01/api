//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace api_2hands.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cart
    {
        public int id { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> productId { get; set; }
        public Nullable<int> qty { get; set; }
        public string productName { get; set; }
        public Nullable<double> productPrice { get; set; }
        public string productImage { get; set; }
        public string color { get; set; }
        public string capacity { get; set; }
    
        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}
