using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    public class PlatformTracker
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public long TotalPurchase { get; set; }
    }
}
