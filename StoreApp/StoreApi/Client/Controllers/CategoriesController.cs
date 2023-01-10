using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.DAL;
using StoreApi.Admin.Dtos.CategoryDtos;
using StoreApi.Admin.Dtos.ProductDtos;

namespace StoreApi.Client.Controllers
{
    [ApiExplorerSettings(GroupName = "user")]
    [Route("user/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(StoreDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAl()
        {
            var categories = _context.Categories.ToList();
            var dto = _mapper.Map< List<CategoryListItemDto>>(categories);
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            var dto = _mapper.Map<CategoryGetDto>(category);
            return Ok(dto);
        }

        
    }
}
