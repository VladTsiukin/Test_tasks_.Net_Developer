using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Context;
using StoreManagement.EF.Entities;
using StoreManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Service
{
    public class StoreService : IStoreService
    {
        protected readonly StoreManagementEfContext _context;
        protected readonly IMapper _mapper;

        public StoreService(StoreManagementEfContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StoreDto>> GetAll()
        {
            var stores = _context.Stores
                .Include(s => s.StoresProducts)
                .AsQueryable();

            return await _mapper.ProjectTo<StoreDto>(stores).ToListAsync()
                .ConfigureAwait(false);
        }

        public IQueryable<StoreDto> GetAllQuery()
        {
            var stores = _context.Stores
                .Include(s => s.StoresProducts)
                .AsQueryable();

            if (stores != null)
            {
                return _mapper.ProjectTo<StoreDto>(stores)
                    .AsNoTracking();
            }

            return null;
        }
    }
}
