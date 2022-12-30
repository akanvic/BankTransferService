using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Utilities
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string errorMessage)
            : base(String.Format("Error: {0}", errorMessage))
        {

        }
    }
}
