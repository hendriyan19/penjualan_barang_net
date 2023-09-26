using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Item
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public string Item_Name { get; set; }

        [Required]
        public long Item_Price { get; set; }
        public bool System_Deleted { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}