using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CustomerOrders
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
    }
}
