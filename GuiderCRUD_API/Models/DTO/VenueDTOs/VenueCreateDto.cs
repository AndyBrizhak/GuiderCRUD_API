using GuiderCRUD_API.Models.DTO.TagDTOs;

namespace GuiderCRUD_API.Models.DTO
{
    public class VenueCreateDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? Descriprion { get; set; }
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
