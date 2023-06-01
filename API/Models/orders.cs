using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class orders
    {
        [Key]
        public long ID { get; set; }
        public DateTime Order_Date { get; set; }

        [ForeignKey("customers")]
        public long? Customer_Id { get; set; }
        public virtual customers Customers { get; set; }

        public virtual ICollection<orders_item> Orders_Items { get; set; }
    }
}
