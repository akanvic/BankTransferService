using System;
using System.Net;

namespace BankTransferService.Core.Responses
{
    public class ResponseModel/*<T> where T : IComparable*/
    {
        public HttpStatusCode? StatusCode { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

    }
}

