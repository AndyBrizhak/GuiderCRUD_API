using GuiderCRUD_API.Models.DTO.TagDTOs;

namespace GuiderCRUD_API.Models.DTO
{
    public class VenueUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Descriprion { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<TagDto>? Tags { get; set; }
    }
}
