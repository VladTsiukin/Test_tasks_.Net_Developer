using StoreManagement.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Interface
{
    public interface IStoreService
    {
        IQueryable<StoreDto> GetAllQuery();
        Task<List<StoreDto>> GetAll();
    }
}