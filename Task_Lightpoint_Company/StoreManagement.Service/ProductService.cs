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
    public class ProductService : IProductService
    {
        protected readonly StoreManagementEfContext _context;
        protected readonly IMapper _mapper;

        public ProductService(StoreManagementEfContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(ProductDto entity)
        {
            var result = await _context.Set<Product>().AddAsync(_mapper.Map<Product>(entity));
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> AddAsync(ProductDto entity, int storeId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var productValue = await _context.Set<Product>().AddAsync(_mapper.Map<Product>(entity));
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    var store = await _context.FindAsync<Store>(storeId);

                    if (store == null)
                    {
                        transaction.Rollback();
                        throw new NullReferenceException(nameof(storeId));
                    }

                    var resultStoreProduct = await _context.Set<StoreProduct>()
                        .AddAsync(new StoreProduct()
                        {
                            Product = productValue.Entity,
                            Store = store
                        });
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);

                    return _mapper.Map<ProductDto>(productValue.Entity);                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(ProductDto entity)
        {
            _context.Set<Product>().Remove(_mapper.Map<Product>(entity));
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<List<ProductDto>> GetAllByIds(IEnumerable<int> ids)
        {
            var products = new List<Product>();

            foreach (var id in ids)
            {
                products.Add(await _context.Products.FindAsync(id)
                    .ConfigureAwait(false));
            }

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetAllByStoreId(int id)
        {
            var sp = await _context.StoresProducts
                             .Where(sp => sp.StoreId == id)
                                .Include(sp => sp.Product)
                                    .Select(sp => sp.Product)
                                    .ToListAsync()
                             .ConfigureAwait(false);

            return _mapper.Map<List<ProductDto>>(sp);
        }

        public async Task<bool> UpdateAsync(ProductDto entity)
        {
            _context.Products.Update(_mapper.Map<Product>(entity));
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}