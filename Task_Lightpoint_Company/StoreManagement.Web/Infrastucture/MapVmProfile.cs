using AutoMapper;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Entities;
using StoreManagement.Web.Models;
using StoreManagement.Web.Models.Product;
using StoreManagement.Web.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Web.Infrastucture
{
    public class MapVmProfile : Profile
    {
        public MapVmProfile()
        {
            // view models
            CreateMap<StoreDto, StoreVM>()
                .MaxDepth(1);
            CreateMap<ProductDto, ProductVM>()
                .MaxDepth(1);
            CreateMap<ProductVM, ProductDto>()
                .ForMember(dto => dto.StoresIds, opt => opt.Ignore());
            CreateMap<CreateProductVM, ProductDto>()
                .ForMember(dto => dto.Name, map => map.MapFrom(vm => vm.ProductName.Trim()))
                .ForMember(dto => dto.Description, map => map.MapFrom(vm => vm.ProductDescription.Trim()))
                .ForSourceMember(sm => sm.StoreId, opt => opt.DoNotValidate())
                .ForMember(dto => dto.StoresIds, opt => opt.Ignore());
            CreateMap<EditProductVM, ProductDto>()
                .ForMember(dto => dto.Name, map => map.MapFrom(vm => vm.ProductName.Trim()))
                .ForMember(dto => dto.Description, map => map.MapFrom(vm => vm.ProductDescription.Trim()))
                .ForMember(dto => dto.StoresIds, opt => opt.Ignore());
            CreateMap<StoreProductsDto, StoreProductVM>();
        }
    }
}