using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.EF.Context;
using StoreManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Service
{
    public class EfCrudService<T> : ICrudService<T> where T : class
    {
        protected readonly StoreManagementEfContext _context;
        protected readonly IMapper _mapper;

        // var data = Mapper.Map<Source, Destination>(dest, opt => opt.ConstructServicesUsing(type => Request.GetDependencyScope().GetService(typeof(YourServiceTypeToConstruct))));

        public EfCrudService(StoreManagementEfContext context,
                             IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            var res = await _context.Set<T>().ToListAsync();
            return res;
        }

        public IQueryable<T> GetAllQuery()
        {
            return _context.Set<T>().AsQueryable<T>();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}