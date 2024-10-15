using AutoMapper;
using GuiderCRUD_API.Data;
using GuiderCRUD_API.Models.DTO.TagDTOs;
using GuiderCRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiderCRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
        {
            var tags = await _context.Tags.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TagDto>(tag));
        }
                
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDto tagCreateDto)
        {
            // Проверка на корректность переданных данных
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Использование AutoMapper для преобразования TagCreateDto в модель Tag
            var tag = _mapper.Map<Tag>(tagCreateDto);

            // Поиск заведений по переданным ID
            if (tagCreateDto.VenueIds != null && tagCreateDto.VenueIds.Any())
            {
                var venues = await _context.Venues
                    .Where(v => tagCreateDto.VenueIds.Contains(v.Id))
                    .ToListAsync();

                // Связывание заведений с тегом
                tag.Venues = venues;
            }

            // Добавление нового тега в контекст базы данных
            _context.Tags.Add(tag);

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Возвращаем ответ с созданным тегом
            var tagDto = _mapper.Map<TagDto>(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tagDto.Id }, tagDto);
        }


        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, TagUpdateDto updateTagDto)
        {
            if (id != updateTagDto.Id)
            {
                return BadRequest();
            }

            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            _mapper.Map(updateTagDto, tag);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
