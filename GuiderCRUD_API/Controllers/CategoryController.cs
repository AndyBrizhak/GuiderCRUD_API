using AutoMapper;
using GuiderCRUD_API.Data;
using GuiderCRUD_API.Models;
using GuiderCRUD_API.Models.DTO.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GuiderCRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryController(ApplicationDbContext db, IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            //return await _db.Categories.ToListAsync();
            IEnumerable<Category> categoryList = await _db.Categories.ToListAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categoryList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            //var category = await _db.Categories.FindAsync(id);
            var category = await _db.Categories
        .Include(c => c.Venues) // Включаем связанные заведения
        .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok (_mapper.Map<CategoryDto>(category));
        }

        // POST: api/Category
        [HttpPost]
        //public async Task<ActionResult<CategoryDto>> PostCategory(CategorCreateDto createCategoryDto)
        //{
        //    var category = new Category
        //    {
        //        Name = createCategoryDto.Name,
        //        Description = createCategoryDto.Description
        //    };

        //    _db.Categories.Add(category);
        //    await _db.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        //}
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            // Проверка на корректность переданных данных
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Создаем новый объект Category с помощью AutoMapper
            var newCategory = _mapper.Map<Category>(categoryCreateDto);

            // Если есть связанные заведения, получаем их по ID
            if (categoryCreateDto.VenueIds != null && categoryCreateDto.VenueIds.Any())
            {
                var venues = await _db.Venues
                    .Where(v => categoryCreateDto.VenueIds.Contains(v.Id))
                    .ToListAsync();

                // Добавляем связанные заведения к категории
                newCategory.Venues = venues;
            }

            // Добавляем категорию в контекст
            _db.Categories.Add(newCategory);

            // Сохраняем изменения в базе данных
            await _db.SaveChangesAsync();

            // Возвращаем созданный объект с кодом 201 (Created) и ссылкой на него
            //return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
            return Ok();
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateDto updateCategoryDto)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            _db.Entry(category).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

            // DELETE: api/Category/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCategory(int id)
            {
                var category = await _db.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();

                return NoContent();
            }

        private bool CategoryExists(int id)
        {
            return _db.Categories.Any(e => e.Id == id);
        }

    }
}
