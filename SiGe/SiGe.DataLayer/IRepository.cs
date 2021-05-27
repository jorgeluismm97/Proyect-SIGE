using System.Threading.Tasks;
using System.Collections.Generic;

namespace SiGe
{
    public interface IRepository<T>
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
