using System;
using System.Data;

namespace BankTransferService.Repo.Dapper.Infrastructure
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
    }
}
