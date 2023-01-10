

using AutoMapper;
using Store.Core.Entities;
using StoreApi.Admin.Dtos.CategoryDtos;
using StoreApi.Admin.Dtos.ProductDtos;

namespace StoreApi.Admin.Profiles
{
    public class AdminMapper:Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public AdminMapper(IHttpContextAccessor httpAccessor)
        {
            CreateMap<Category,CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryListItemDto>();

            CreateMap<Category, CategoryInProductGetDto>();
            CreateMap<Product, ProductGetDto>()
                .ForMember(x => x.ImgUrl, f => f.MapFrom(x => $"{_httpAccessor.HttpContext.Request.Scheme}://{_httpAccessor.HttpContext.Request.Host}{_httpAccessor.HttpContext.Request.PathBase}/Uploads/Products/{x.ImgUrl}"));
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductListItemDto>();


            this._httpAccessor = httpAccessor;
        } 
    }
}
