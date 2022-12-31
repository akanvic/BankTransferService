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
        //private readonly IHttpClientFactory _httpClientFactory;
        //public HTTPClientHelper(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        public HttpClient Initialize(string secretKey, string baseUrl, IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + secretKey);
            return client;
        }
    }
}
