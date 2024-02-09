using System.ComponentModel.DataAnnotations;

namespace Hangfire_Service.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }

        public CustomerDetails CustomerDetails { get; set; }
    }
}