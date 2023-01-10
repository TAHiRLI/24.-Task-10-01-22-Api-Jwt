using AutoMapper;
using Store.Core.Entities;
using StoreApi.Admin.Dtos.CategoryDtos;
using StoreApi.Admin.Dtos.ProductDtos;

namespace StoreApi.Client.Profiles
{
    public class ClientMapper:Profile
    {
        public ClientMapper()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryListItemDto>();

            CreateMap<Product, ProductGetDto>();
            CreateMap<Product, ProductListItemDto>();
            CreateMap<Category, CategoryInProductGetDto>();

        }

    }
}
