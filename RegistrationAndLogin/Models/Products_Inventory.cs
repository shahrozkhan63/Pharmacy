//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegistrationAndLogin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products_Inventory
    {
        public int ProductIDD { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<decimal> Rate_buy { get; set; }
        public Nullable<System.DateTime> mfg_date { get; set; }
        public Nullable<System.DateTime> exp_date { get; set; }
        public int supplier_id { get; set; }
        public int manufacturer_id { get; set; }
        public string formula { get; set; }
        public string description { get; set; }
    
        public virtual manufacturer manufacturer { get; set; }
        public virtual supplier supplier { get; set; }
    }
}