using GuiderCRUD_API.Models.DTO.VenueDTOs;

namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        // Список заведений, которые имеют этот тег
        public List<VenueDto>? Venues { get; set; }
    }
}
