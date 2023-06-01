using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class customers
    {
        [Key]
        public long ID { get; set; }
        public string? Customer_Phone { get; set; }
        public string? Customer_Email { get; set; }

    }
}
