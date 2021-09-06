using System.Collections.Generic;
using Items.Models;

namespace Items.Repository
{
    public interface IItemRepository : Irepository<Item>
    {
        IEnumerable<Item> GetParents();
    }
}