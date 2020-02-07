using AutoMapper;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Context;
using StoreManagement.EF.Entities;
using StoreManagement.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Service
{
    public class StoreProductService : IStoreProductService
    {
        protected readonly StoreManagementEfContext _context;
        protected readonly IMapper _mapper;

        public StoreProductService(StoreManagementEfContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StoreProductsDto> AddAsync(StoreProductsDto entity)
        {
            var result = await _context.Set<StoreProduct>().AddAsync(_mapper.Map<StoreProduct>(entity));
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<StoreProductsDto>(result);
        }

        public async Task<bool> DeleteAsync(StoreProductsDto entity)
        {
            _context.Set<StoreProduct>().Remove(_mapper.Map<StoreProduct>(entity));
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
