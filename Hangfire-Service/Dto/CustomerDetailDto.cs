namespace Hangfire_Service.Dto
{
    public class CustomerDetailDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int OrderNumber { get; set; }
    }
}
