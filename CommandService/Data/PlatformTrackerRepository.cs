using CommandService.Dtos;
using Microsoft.EntityFrameworkCore;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformTrackerRepository : IPlatformTrackerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlatformTrackerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlatformTracker(PlatformCreateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var tracker = new PlatformTracker
            {
                Cost = request.Cost,
                Name = request.Name,
                Publisher = request.Publisher
            };

            _context.PlatformTracker.Add(tracker);
            return (await _context.SaveChangesAsync() >= 0);

        }

        public async Task<List<TrackerResponseDto>> GetPlatformTracker()
        {
            var list = await _context.PlatformTracker.
                Select(x => new TrackerResponseDto
                {
                    Cost = x.Cost,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    Name = x.Name,
                    Publisher = x.Publisher,
                    TotalPurchase = x.TotalPurchase
                }).ToListAsync();
            return list;
        }
    }
}
