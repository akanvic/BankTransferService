using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Utilities
{
    public class HTTPClientHelper
    {
        public HTTPClientHelper()
        {
            ClientHandler = new HttpClientHandler();
            ClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient(ClientHandler);
        }

        public HttpClientHandler ClientHandler { get; set; }

        public HttpClient client { get; set; }

        public HttpClient Initialize(string access_token)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("access_token", access_token);
            return client;
        }
    }
}
