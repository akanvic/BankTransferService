using BankTransferService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Repo.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
                                                        : base(options)
        {
        }

        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}
