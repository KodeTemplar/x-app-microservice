using CommandService.Dtos;
using PlatformService.Dtos;

namespace PlatformService.Data
{
    public interface IPlatformTrackerRepository
    {
        Task<bool> CreatePlatformTracker(PlatformCreateDto request);
        Task<List<TrackerResponseDto>> GetPlatformTracker();
    }
}
