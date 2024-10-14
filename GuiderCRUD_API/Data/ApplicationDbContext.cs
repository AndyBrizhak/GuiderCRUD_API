using GuiderCRUD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GuiderCRUD_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношения "многие ко многим" без явной промежуточной таблицы
            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Tags)
                .WithMany(t => t.Venues);
        }
    }
}
