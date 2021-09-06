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
                    Name = "Item 1"
                },
                new Item
                {
                    ID = 2,
                    ParentId = null,
                    Name = "Item 2"
                },
                new Item
                {
                    ID = 3,
                    ParentId = null,
                    Name = "Item 3"
                },
                new Item
                {
                    ID = 4,
                    ParentId = 1,
                    Name = "Item 4"
                },
                new Item
                {
                    ID = 5,
                    ParentId = 1,
                    Name = "Item 5"
                },
                new Item
                {
                    ID = 6,
                    ParentId = 2,
                    Name = "Item 6"
                },
                new Item
                {
                    ID = 7,
                    ParentId = 3,
                    Name = "Item 7"
                },
                new Item
                {
                    ID = 8,
                    ParentId = 3,
                    Name = "Item 8"
                }
            );
        }
    }
}