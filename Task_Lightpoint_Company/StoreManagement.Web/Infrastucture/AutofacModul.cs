using Autofac;
using AutoMapper;
using StoreManagement.Core.Dto;
using StoreManagement.Interface;
using StoreManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StoreManagement.Web.Infrastucture
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the `UseServiceProviderFactory(new AutofacServiceProviderFactory())` that happens in Program and registers Autofac
            // as the service provider.
            builder.RegisterGeneric(typeof(EfCrudService<>))
                .As(typeof(ICrudService<>))
                .InstancePerDependency();

            builder.RegisterType<StoreService>().As<IStoreService>();
            builder.RegisterType<ProductService>().As<IProductService>();

            //builder.RegisterAssemblyTypes(typeof(CardsService).Assembly)
            //    .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(typeof(CardService).Assembly)
            //   .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
        }
    }
}