namespace GuiderCRUD_API.Models.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
