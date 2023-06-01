using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class items
    {
        [Key]
        public long? ID { get; set; }
        public string? Item_Name { get; set; }
        public long? Item_Price { get; set; }

        public virtual ICollection<orders_item> Orders_Items { get; set; }
    }
}
