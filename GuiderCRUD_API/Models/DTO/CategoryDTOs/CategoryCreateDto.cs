﻿namespace GuiderCRUD_API.Models.DTO.CategoryDTOs
{
    public class CategorCreateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
