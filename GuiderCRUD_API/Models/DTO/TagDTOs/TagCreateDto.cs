namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagCreateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
