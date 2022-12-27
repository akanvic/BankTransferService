using System;
using System.Data;

namespace BankTransferService.Repo.Infrastructure
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
    }
}
