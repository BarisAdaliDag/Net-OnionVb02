using AutoMapper;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.MappingProfiles
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<AppUserDto, AppUser>().ReverseMap();
            CreateMap<AppUserProfileDto, AppUserProfile>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();

            CreateMap<ProductAttributeDto, ProductAttribute>().ReverseMap();
            CreateMap<ProductAttributeValueDto, ProductAttributeValue>().ReverseMap();
            CreateMap<ProductWithAttributesDto, Product>().ReverseMap();
        }
    }
}
