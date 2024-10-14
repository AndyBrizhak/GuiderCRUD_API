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
        public async Task<ActionResult<VenueDto>> PostVenue(VenueCreateDto createVenueDto)
        {
            var venue = _mapper.Map<Venue>(createVenueDto);

            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();

            var venueDto = _mapper.Map<VenueDto>(venue);
            return CreatedAtAction(nameof(GetVenue), new { id = venue.Id }, venueDto);
        }

        // PUT: api/Venue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(int id, VenueUpdateDto updateVenueDto)
        {
            if (id != updateVenueDto.Id)
            {
                return BadRequest();
            }

            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            _mapper.Map(updateVenueDto, venue);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
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

