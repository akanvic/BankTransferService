using BankTransferService.Core.Entities;
using BankTransferService.Repo.Data.GenericRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Repo.Data.Repository.Interfaces
{
    public interface ITransactionRepo : IGenericRepository<TransactionHistory>
    {

    }
}
