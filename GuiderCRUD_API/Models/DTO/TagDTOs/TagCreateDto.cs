namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        // ID заведений, которые будут связаны с этим тегом при создании
        public List<int>? VenueIds { get; set; }
    }
}
