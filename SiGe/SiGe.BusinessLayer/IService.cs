using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGe
{
    public interface IService<T>
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
