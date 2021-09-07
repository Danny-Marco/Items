using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Items.Models
{
    public class ItemViewModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Ссылка на родителя")]
        public int? ParentId { get; set; }

        public List<Item> Children { get; set; }
    }
}