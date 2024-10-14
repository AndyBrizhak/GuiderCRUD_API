using GuiderCRUD_API.Models.DTO.TagDTOs;

namespace GuiderCRUD_API.Models.DTO.VenueDTOs
{
    public class VenueDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? Descriprion { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public List<TagDto>? Tags { get; set; }
    }
}
