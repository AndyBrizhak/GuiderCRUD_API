namespace GuiderCRUD_API.Models.DTO.TagDTOs
{
    public class TagUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        // ID заведений, с которыми тег должен быть связан после обновления
        public List<int>? VenueIds { get; set; }
    }
}
