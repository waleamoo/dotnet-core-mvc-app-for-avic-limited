using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvicLimited.Data.Models
{
    public class Quote : BaseEntity
    {
        public string QuoteName { get; set; } = string.Empty;
        public string QuoteEmail { get; set; } = string.Empty;
        public string QuotePhone { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int QuoteBudget { get; set; }
        public string? QuoteMessage { get; set; } = string.Empty;
    }
}
