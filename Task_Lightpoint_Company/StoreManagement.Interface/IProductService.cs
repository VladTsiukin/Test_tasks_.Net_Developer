using StoreManagement.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Interface
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllByIds(IEnumerable<int> ids);
        Task<List<ProductDto>> GetAllByStoreId(int id);
        Task<ProductDto> AddAsync(ProductDto entity);
        Task<bool> UpdateAsync(ProductDto entity);
        Task<bool> DeleteAsync(ProductDto entity);
        Task<ProductDto> AddAsync(ProductDto entity, int storeId);
    }
}