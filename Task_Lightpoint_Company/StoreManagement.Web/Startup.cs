using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Context;
using StoreManagement.EF.Entities;
using StoreManagement.Service.Utils;
using StoreManagement.Web.Controllers;
using StoreManagement.Web.Infrastucture;
using StoreManagement.Web.Models;
using StoreManagement.Web.Models.Store;

namespace StoreManagement.Web
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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            // add db:
            services.AddDbContext<StoreManagementEfContext>(optoins =>
                optoins.UseSqlServer(Configuration.GetConnectionString("StoreManagementEfContext"),
                    m => m.MigrationsAssembly("StoreManagement.EF")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRouting(option =>
            {
                option.LowercaseQueryStrings = true;
                option.LowercaseUrls = true;
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<HomeController>().PropertiesAutowired();
            builder.RegisterModule(new AutofacModule());
            builder.AddAutoMapper(cfg => 
                {
                    cfg.AddProfile<MapDtoProfile>();
                    cfg.AddProfile<MapVmProfile>();
                },
                typeof(Store).Assembly, 
                typeof(StoreDto).Assembly, 
                typeof(StoreVM).Assembly);          
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
