//using BankTransferService.Repo.Data.Repository.Interfaces;
//using BankTransferService.Service.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankTransferService.Service.Implementation
//{
//    public class BankTransferFactory
//    {
//        private readonly ITransactionRepo _transactionRepo;
//        private readonly IHttpClientFactory _httpClientFactory;
//        public BankTransferFactory(ITransactionRepo transactionRepo, IHttpClientFactory httpClientFactory)
//        {
//            _transactionRepo = transactionRepo;
//            _httpClientFactory = httpClientFactory;
//        }

//        public IBankTransferService GetProviderGateway(string provider)
//        {
//            IBankTransferService bankTransferService;
//            if (provider.ToLower().Equals("paystack"))
//            {
//                bankTransferService = new PaystackGateway(_transactionRepo, _httpClientFactory);
//            }
//            else if (provider.ToLower().Equals("flutterwave"))
//            {
//                bankTransferService = new FlutterwaveGateway(_httpClientFactory);
//            }
//            else
//            {
//                bankTransferService = new PaystackGateway(_transactionRepo, _httpClientFactory);
//            }
//            return bankTransferService;
//        }
//    }
//}
