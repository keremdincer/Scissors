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
    
    public partial class ShortUrl
    {
        public ShortUrl()
        {
            this.ShortUrlUsages = new HashSet<ShortUrlUsage>();
        }
    
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string Url { get; set; }
        public bool isPermanent { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public int ApiKeyId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ExpiresAt { get; set; }
    
        public virtual ApiKey ApiKey { get; set; }
        public virtual ICollection<ShortUrlUsage> ShortUrlUsages { get; set; }
    }
}