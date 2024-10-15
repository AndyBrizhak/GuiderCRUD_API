namespace GuiderCRUD_API.Models.DTO.CategoryDTOs
{
    public class CategoryCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<int>? VenueIds { get; set; } // Список ID заведений (необязательно)
    }
}
