using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Data.DAL;
using StoreApi.Admin.Dtos.ProductDtos;
using StoreApi.Helpers;
using System.Data;

namespace StoreApi.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpAccessor;

        public ProductsController(StoreDbContext context, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor httpAccessor)
        {
            this._context = context;
            this._mapper = mapper;
            this._env = env;
            this._httpAccessor = httpAccessor;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();

            List<ProductListItemDto> list = _mapper.Map<List<ProductListItemDto>>(products);

            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product =_context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductGetDto>(product);
            //string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/uploads/products/";
            //dto.ImgUrl = baseUrl + product.ImgUrl;

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductPostDto dto)
        {
            if (!_context.Categories.Any(x => x.Id == dto.CategoryId))
            {
                return BadRequest();
            }

            Product product = _mapper.Map<Product>(dto);
            product.ImgUrl = FileManager.Save(dto.ImageFile, _env.WebRootPath, "Uploads/Products");
            _context.Products.Add(product);
            _context.SaveChanges();


            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Edit( int id,[FromForm] ProductPutDto dto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return BadRequest();
            if (!_context.Categories.Any(x => x.Id == dto.CategoryId))
                return BadRequest();

            product.Name = dto.Name;
            product.SalePrice = dto.SalePrice;
            product.CostPrice = dto.CostPrice;
            product.DiscountPercent = dto.DiscountPercent;  
            product.StockStatus = dto.StockStatus;  
            product.CategoryId = dto.CategoryId;

            if(dto.ImageFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "Uploads/Product", product.ImgUrl);
                product.ImgUrl = FileManager.Save(dto.ImageFile, _env.WebRootPath, "Uploads/Products");
            }

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
