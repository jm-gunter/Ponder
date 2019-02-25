using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ponder.Data
{
    public interface IDataContext<T>
    {
        Task CreateAsync(T record);

        Task<IEnumerable<T>> ReadAsync();

        Task<IEnumerable<T>> ReadAsync(Expression<Func<T, bool>> filter);

        Task<T> UpdateAsync(Expression<Func<T, bool>> filter, T obj);

        Task DeleteAsync(Expression<Func<T, bool>> filter);
    }
}
