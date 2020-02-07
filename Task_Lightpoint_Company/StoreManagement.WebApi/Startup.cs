using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Context;
using StoreManagement.EF.Entities;
using StoreManagement.Service.Utils;
using StoreManagement.Web.Infrastucture;
using StoreManagement.WebApi.Controllers;

namespace StoreManagement.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();

            // add db:
            services.AddDbContext<StoreManagementEfContext>(optoins =>
                optoins.UseSqlServer(Configuration.GetConnectionString("StoreManagementEfContext"),
                    m => m.MigrationsAssembly("StoreManagement.EF")));

            services.AddRouting(option =>
            {
                option.LowercaseQueryStrings = true;
                option.LowercaseUrls = true;
            });
        }

        public void ConfigureContainer(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<ManagementApiController>().PropertiesAutowired();
            builder.RegisterModule(new AutofacModule());
            builder.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MapDtoProfile>();
                cfg.AddProfile<MapVmProfile>();
            },
                typeof(Store).Assembly,
                typeof(StoreDto).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
