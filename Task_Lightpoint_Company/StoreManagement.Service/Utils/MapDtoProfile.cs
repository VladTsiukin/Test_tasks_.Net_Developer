using AutoMapper;
using StoreManagement.Core.Dto;
using StoreManagement.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StoreManagement.Service.Utils
{
    public class MapDtoProfile : Profile
    {
        public MapDtoProfile()
        {
            // DTOs
            CreateMap<Store, StoreDto>()
                .MaxDepth(1)
                .ForMember(dto => dto.ProductsIds,
                    map => map.MapFrom(ef => ef.StoresProducts.Select(p => p.ProductId)));
            CreateMap<Product, ProductDto>()
                .MaxDepth(1)
                .ForMember(dto => dto.StoresIds,
                map => map.MapFrom(ef => ef.StoresProducts.Select(p => p.StoreId)));
            CreateMap<ProductDto, Product>()
                .ForMember(dto => dto.StoresProducts, opt => opt.Ignore());
            CreateMap<StoreProduct, StoreProductsDto>();
        }
    }
}