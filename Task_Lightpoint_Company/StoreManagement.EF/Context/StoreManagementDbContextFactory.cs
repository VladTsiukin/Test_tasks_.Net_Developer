using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace StoreManagement.EF.Context
{
    class StoreManagementDbContextFactory : IDesignTimeDbContextFactory<StoreManagementEfContext>
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=storemanagement_db;Trusted_Connection=True;MultipleActiveResultSets=true";

        public StoreManagementEfContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreManagementEfContext>();
            optionsBuilder.UseSqlServer(ConnectionString,
                b => b.MigrationsAssembly("StoreManagement.EF"));

            return new StoreManagementEfContext(optionsBuilder.Options);
        }
    }
}
