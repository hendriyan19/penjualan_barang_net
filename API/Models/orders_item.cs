using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class orders_item
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("orders")]
        public long? Order_Id { get; set; }

        [ForeignKey("items")]
        public long? Item_Id { get; set; }
    }
}
