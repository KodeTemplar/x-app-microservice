using Models;

namespace Hangfire_Service.Models
{
    public class CustomerDetails
    {
        public CustomerDetails()
        {
            CustomerOrders = new HashSet<CustomerOrders>();
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }

        public virtual ICollection<CustomerOrders> CustomerOrders { get; set; }
        public virtual Country Country { get; set; }
    }
}
