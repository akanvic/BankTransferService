using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Core.Entities
{
    public class TransactionHistory
    {
        [Key]
        public int TransactionId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientBank { get; set; }
        public string RecipientAcccountNumber { get; set; }
        public string RecipientBankCode { get; set; }
        public string TransactionReference { get; set; }
        public string TransferCode { get; set; }
        public string TransactionStatus { get; set; }
        public string? Currency { get; set; }
        public string? Source { get; set; }
        public string? Reason { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? SessionId { get; set; }
        public int? MaxRetryAttempt { get; set; }
        public string? Provider { get; set; }
    }
}
