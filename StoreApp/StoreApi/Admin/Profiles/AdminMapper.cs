

using AutoMapper;
using Store.Core.Entities;
using StoreApi.Admin.Dtos.CategoryDtos;
using StoreApi.Admin.Dtos.ProductDtos;

namespace StoreApi.Admin.Profiles
{
    public class AdminMapper:Profile
    {
        public AdminMapper()
        {
            CreateMap<Category,CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryListItemDto>();

            CreateMap<Category, CategoryInProductGetDto>();
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductListItemDto>();
        } 
    }
}
