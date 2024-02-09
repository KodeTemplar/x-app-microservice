using Hangfire_Service.Data;
using Hangfire_Service.Dto;
using Microsoft.EntityFrameworkCore;

namespace Hangfire_Service.DATABASEQUERYOPTIMIZATION
{
    public class DatabaseOptimization
    {
        private readonly ApplicationDbContext _context;
        public DatabaseOptimization(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerDetailDto>> GetLargeRecord()
        {

            var query = await _context.CustomerDetails
                .Include(c => c.Country) // Eager loading related entities
                .Where(c => c.CountryId == 10) // Filtering
                .OrderBy(c => c.LastName) // Sorting
                .Select(x => new CustomerDetailDto
                {
                    Email = x.Email,
                    Country = x.Country.Name,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id
                }).ToListAsync(); // Retrieve only the first matching record
            return query;


            /////SQL VERSION//////

            /*SELECT
                c.Email,
                co.Name AS Country,
                c.FirstName,
                c.LastName,
                c.Id
            FROM
                CustomerDetails c
            JOIN
                Country co ON c.CountryId = co.Id
            WHERE
                c.CountryId = 10
            ORDER BY
                c.LastName;*/

        }
    }
}
