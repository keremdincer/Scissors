using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scissors.Dtos
{
    public class ShortUrlUsageDto
    {
        public int ShortUrlId { get; set; }
        public string ClientIP { get; set; }
        public string ClientBrowser { get; set; }
        public string ClientDevice { get; set; }
        public string ClientOS { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}