namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
