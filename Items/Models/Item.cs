using System.ComponentModel.DataAnnotations;

namespace Items.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Ссылка на родителя")]
        public int? ParentId { get; set; }
    }
}