using Microsoft.AspNetCore.Mvc;

namespace Lapshop.Repositories
{
    public interface ILapshopRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetOneById(int? id);
        Task Create(T Entity);
        Task Update(T Entity);
        Task Delete(T Entity);
    }
}
