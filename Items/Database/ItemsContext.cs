using Items.Models;
using Microsoft.EntityFrameworkCore;

namespace Items.Database
{
    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    ID = 1,
                    ParentId = null,
                    Name = "Technique"
                },
                new Item
                {
                    ID = 2,
                    ParentId = null,
                    Name = "Furniture"
                },
                new Item
                {
                    ID = 3,
                    ParentId = 1,
                    Name = "TV sets"
                },
                new Item
                {
                    ID = 4,
                    ParentId = 3,
                    Name = "Samsung"
                },
                new Item
                {
                    ID = 5,
                    ParentId = 3,
                    Name = "LG"
                },
                new Item
                {
                    ID = 6,
                    ParentId = 2,
                    Name = "Armchairs"
                },
                new Item
                {
                    ID = 7,
                    ParentId = 2,
                    Name = "Sofas"
                }
            );
        }
    }
}