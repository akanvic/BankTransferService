using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Core.Entities
{
    public class ReadConfig
    {
        public string DefaultConnection { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience1 { get; set; }
        public string Audience2 { get; set; }
        public string TokenSpanMinutes { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
