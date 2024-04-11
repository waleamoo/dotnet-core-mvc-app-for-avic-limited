using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvicLimited.Data.Models
{
    public class Contact : BaseEntity
    {
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactSubject { get; set; } = string.Empty;
        public string ContactMessage { get; set; } = string.Empty;

    }
}
