using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platform);
        Task<List<SalesTracker>> GetSalesTracker();
    }
}
