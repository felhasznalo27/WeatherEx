//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherEx2.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class TUser2City
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public bool FavoriteCity { get; set; }
        public System.DateTime LastModified { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual TImportedCity TImportedCity { get; set; }
    }
}
