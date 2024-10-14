using AutoMapper;
using GuiderCRUD_API.Data;
using GuiderCRUD_API.Models.DTO.VenueDTOs;
using GuiderCRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuiderCRUD_API.Models.DTO;

namespace GuiderCRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VenueController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Venue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueDto>>> GetVenues()
        {
            var venues = await _context.Venues.Include(v => v.Category).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VenueDto>>(venues));
        }

        // GET: api/Venue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VenueDto>> GetVenue(int id)
        {
            var venue = await _context.Venues.Include(v => v.Category).FirstOrDefaultAsync(v => v.Id == id);

            if (venue == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VenueDto>(venue));
        }

        // POST: api/Venue
        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(VenueCreateDto venueCreateDto)
        {
            // Найти категорию по CategoryId
            var category = await _context.Categories.FindAsync(venueCreateDto.CategoryId);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            // Найти теги по списку TagIds
            var tags = await _context.Tags
                .Where(tag => venueCreateDto.TagIds.Contains(tag.Id))
                .ToListAsync();

            if (!tags.Any())
            {
                return NotFound(new { message = "No valid tags found" });
            }

            // Создать новое Venue и установить связи с категорией и тегами
            var venue = new Venue
            {
                Name = venueCreateDto.Name,
                Address = venueCreateDto.Address,
                Descriprion = venueCreateDto.Descriprion,
                Category = category, // Связь с категорией
                Tags = tags          // Связь с тегами
            };

            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();

            // Вернуть результат с созданным объектом
            //return CreatedAtAction(nameof(GetVenue), new { id = venue.Id }, venue);
            return NoContent();
        }

        // PUT: api/Venue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(int id, VenueUpdateDto updateVenueDto)
        {
            if (id != updateVenueDto.Id)
            {
                return BadRequest();
            }

            // Найти существующее заведение по ID
            var venue = await _context.Venues
                .Include(v => v.Tags)
                .FirstOrDefaultAsync(v => v.Id == id);

            //var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound(new { message = "Venue not found" });
            }

            // Найти теги по списку TagIds
            var tags = await _context.Tags
                .Where(tag => updateVenueDto.TagIds.Contains(tag.Id))
                .ToListAsync();

            _mapper.Map(updateVenueDto, venue);

            

            return NoContent();
        }

        // DELETE: api/Venue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.Id == id);
        }
    }
}

