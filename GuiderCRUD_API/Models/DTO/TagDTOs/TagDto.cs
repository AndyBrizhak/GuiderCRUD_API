namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
