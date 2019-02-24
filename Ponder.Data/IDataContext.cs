using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ponder.Data
{
    public interface IDataContext<T>
    {
        Task CreateAsync(T record);

        Task<IEnumerable<T>> ReadAsync();

        Task<IEnumerable<T>> ReadAsync(string filter);

        Task<T> UpdateAsync();

        Task<T> DeleteAsync();
    }
}
