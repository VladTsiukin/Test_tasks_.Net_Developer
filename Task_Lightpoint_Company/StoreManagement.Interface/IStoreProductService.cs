using StoreManagement.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Interface
{
    public interface IStoreProductService
    {
        Task<StoreProductsDto> AddAsync(StoreProductsDto entity);
        Task<bool> DeleteAsync(StoreProductsDto entity);
    }
}