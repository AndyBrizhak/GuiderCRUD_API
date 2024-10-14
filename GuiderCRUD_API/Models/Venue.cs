using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GuiderCRUD_API.Models
{
    public class Venue
    {
        [Key] // Указывает на первичный ключ
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] // Поле обязательно для заполнения
        [MaxLength(200)] // Ограничение на максимальную длину строки
        public required string Name { get; set; }

        [Required] // Поле обязательно для заполнения
        public required string Address { get; set; }

        public string Descriprion { get; set; }

        // Внешний ключ, указывающий на Category (Категория)
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Связь с моделью Category — каждое заведение относится к одной категории
        public required Category Category { get; set; }

        // Связь "многие ко многим" с тегами
        public ICollection<Tag> Tags { get; set; }
    }
}
