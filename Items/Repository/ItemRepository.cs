using System.Collections.Generic;
using System.Linq;
using Items.Database;
using Items.Models;

namespace Items.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemsContext _context;

        public ItemRepository(ItemsContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetParents()
        {
            return _context.Items.Where(i => i.ParentId == null);
        }

        public bool ItemExists(int id)
        {
            return _context.Items.Any(i => i.ID == id);
        }

        public IEnumerable<Item> GetChildrenByID(int id)
        {
            return _context.Items.Where(i => i.ParentId == id);
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items;
        }

        public Item GetById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.ID == id);
            // return _context.Items.FindAsync(id);
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
        }

        public void Update(Item item)
        {
            var foundItem = GetById(item.ID);
            foundItem.ParentId = item.ParentId;
            foundItem.Name = item.Name;
        }

        public void Delete(Item item)
        {
            _context.Items.Remove(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}