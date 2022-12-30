using BankTransferService.Repo.Data.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Repo.Data.GenericRepository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected BankDbContext _BankContext;
 


        public GenericRepository(BankDbContext BankContext)
        {
            _BankContext = BankContext;
        }

        public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _BankContext.Set<T>().AsNoTracking()) : await Task.Run(() => _BankContext.Set<T>());

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _BankContext.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _BankContext.Set<T>().Where(expression));

        public Task<T> CreateAsync(T entity) => Task.Run(() => _BankContext.Set<T>().Add(entity).Entity);

        public async Task Save()
        {
            await _BankContext.SaveChangesAsync();
        }
    }
}
