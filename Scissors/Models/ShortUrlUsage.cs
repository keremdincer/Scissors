//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Scissors.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShortUrlUsage
    {
        public int Id { get; set; }
        public int ShortUrlId { get; set; }
        public string ClientIP { get; set; }
        public string ClientBrowser { get; set; }
        public string ClientDevice { get; set; }
        public string ClientOS { get; set; }
        public System.DateTime CreatedAt { get; set; }
    
        public virtual ShortUrl ShortUrl { get; set; }
    }
}
