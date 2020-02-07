using Microsoft.EntityFrameworkCore;
using StoreManagement.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.EF.Context
{
    public class StoreManagementEfContext : DbContext
    {
        public StoreManagementEfContext(DbContextOptions<StoreManagementEfContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreProduct> StoresProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreProduct>().HasKey(sc => new { sc.ProductId, sc.StoreId });

            // data seeding
            var products = new List<Product>();
            var stores = new List<Store>();
            var storesProducts = new List<StoreProduct>();

            for (int i = 0; i < 100; i++)
            {
                var product = new Product { ProductId = ++i, Name = $"Product{i}", Description = $"Product{i} description" };
                var store = new Store { StoreId = ++i, Name = $"Store{i}", Address = $"Address{i}", StoreHours = "9.00-18.00" };
                var id = ++i;
                products.Add(product);
                stores.Add(store);
                storesProducts.Add(new StoreProduct { Product = product, Store = store, ProductId = id, StoreId = id });
            }

            #region ProductsSeed
            modelBuilder.Entity<Product>()
                .HasData(new Product { ProductId = 1, Name = $"Product1", Description = $"Product1 description" },
                         new Product { ProductId = 2, Name = $"Product2", Description = $"Product2 description" },
                         new Product { ProductId = 3, Name = $"Product3", Description = $"Product3 description" },
                         new Product { ProductId = 4, Name = $"Product4", Description = $"Product4 description" },
                         new Product { ProductId = 5, Name = $"Product5", Description = $"Product5 description" });
            #endregion

            #region StoresSeed
            modelBuilder.Entity<Store>().HasData(
                    new Store { StoreId = 1, Name = $"Store1", Address = $"Address1", StoreHours = "9.00-18.00" },
                    new Store { StoreId = 2, Name = $"Store2", Address = $"Address2", StoreHours = "9.00-21.00" },
                    new Store { StoreId = 3, Name = $"Store3", Address = $"Address3", StoreHours = "9.00-17.00" }
                );
            #endregion

            #region StoreProduct
            modelBuilder.Entity<StoreProduct>().HasData(
                    new StoreProduct { StoreId = 1, ProductId =  1},
                    new StoreProduct { StoreId = 1, ProductId =  2},
                    new StoreProduct { StoreId = 2, ProductId =  3},
                    new StoreProduct { StoreId = 2, ProductId =  5},
                    new StoreProduct { StoreId = 3, ProductId =  3},
                    new StoreProduct { StoreId = 3, ProductId =  5},
                    new StoreProduct { StoreId = 3, ProductId =  4}
                );
            #endregion
        }
    }
}