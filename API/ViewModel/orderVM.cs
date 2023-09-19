using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModel
{
    public class orderVM
    {
        public long ID { get; set; }
        public long Order_Id { get; set; }
        public long Item_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public long Customer_Id { get; set; }

    }
}
