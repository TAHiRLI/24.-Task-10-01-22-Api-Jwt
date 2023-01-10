using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data.DAL;
using StoreApi.Admin.Dtos.ProductDtos;

namespace StoreApi.Client.Controllers
{
    [ApiExplorerSettings(GroupName = "user")]
    [Route("user/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(StoreDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.Include(x => x.Category).ToList();
            var dto = _mapper.Map<List<ProductListItemDto>>(products);
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();
            var dto = _mapper.Map<ProductGetDto>(product);
            return Ok(dto);
        }
    }
}
