using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Entities;
using Store.Data.DAL;
using StoreApi.Admin.Dtos.CategoryDtos;
using System.Diagnostics.Contracts;

namespace StoreApi.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles ="SuperAdmin, Admin")]
    [Route("admin/api/[controller]")]
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
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();

            List<CategoryListItemDto> list = _mapper.Map<List<CategoryListItemDto>>(categories);
            
            return Ok(list);
        }
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);

            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Create(CategoryPostDto dto)
        {
            if (_context.Categories.Any(x => x.Name.ToUpper() == dto.Name.ToUpper()))
                return BadRequest();

            Category category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, CategoryPostDto dto)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }

            if (_context.Categories.Any(x => x.Id != id&&x.Name.ToUpper() == dto.Name.ToUpper()))
                return BadRequest();

            category.Name = dto.Name;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
