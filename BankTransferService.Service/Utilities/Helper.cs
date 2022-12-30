using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Utilities
{
    public static class Helper
    {
        public static string GenerateTransactionReference()
        {
            Guid guid = Guid.NewGuid();

            // Convert the GUID to a string with the format "N"
            string uuid = guid.ToString("N");

            // Truncate the string to 16 digits
            string uuid16 = uuid.Substring(0, 16);

            return uuid16;
        }
        public const string PaystackBaseURL = @"https://api.paystack.co/";
        public const string FlutterwavBaseURL = @"https://api.paystack.co/";
        public const string PaystackSecretKey = "sk_test_877f399e799d8bb2b162e8ea631fc89a17b977cc";
        public const string FlutterwaveSecretKey = "FLWSECK_TEST-81070ed7e3080a289ab067180d5bd542-X";

    }
}
