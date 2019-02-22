using System;
using System.Threading.Tasks;

namespace Ponder.Data
{
    public interface IDataContext<T>
    {
        Task CreateAsync(T record);

        Task<T> ReadAsync();

        Task<T> UpdateAsync();

        Task<T> DeleteAsync();
    }
}
