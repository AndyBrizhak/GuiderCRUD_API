﻿using System.ComponentModel.DataAnnotations;

namespace GuiderCRUD_API.Models
{
    public class Category
    {
        [Key] // Указывает на первичный ключ
        public int Id { get; set; }

        [Required] // Поле обязательно для заполнения
        [MaxLength(100)] // Ограничение на максимальную длину строки
        public required string Name { get; set; }

        public string? Description { get; set; }

        // Связь с таблицей Venue — одна категория имеет много заведений
        public required ICollection<Venue> Venues { get; set; }
    }
}
