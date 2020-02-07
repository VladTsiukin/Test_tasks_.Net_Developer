using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Interface
{
    public interface ICrudService<Entity> where Entity : class
    {
        IQueryable<Entity> GetAllQuery();
        Task<List<Entity>> GetAll();
        Task<Entity> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(Entity entity);
    }
}