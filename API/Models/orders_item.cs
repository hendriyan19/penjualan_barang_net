using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class OrderItem
    {
        [Key]
        public long ID { get; set; }


        [ForeignKey("Order")]
        public long Order_Id { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Item")]
        public long Item_Id { get; set; }
        public virtual Item Item { get; set; }
    }
}