using BankTransferService.Core.Entities;
using BankTransferService.Repo.Data.GenericRepository.Implementations;
using BankTransferService.Repo.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Repo.Data.Repository.Implementations
{
    public class TransactionRepo : GenericRepository<TransactionHistory>, ITransactionRepo
    {
        private readonly BankDbContext _dbContext;
        public TransactionRepo(BankDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
