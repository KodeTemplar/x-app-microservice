using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

namespace CommandService.Controllers
{
    [Route("api/InC/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformTrackerRepository _repo;
        public PlatformsController(IPlatformTrackerRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> TestInboundConnection([FromBody] PlatformCreateDto request)
        {
            Console.WriteLine("--> Inbound post #Command Service");
            var result = await _repo.CreatePlatformTracker(request);
            if (result)
            {
                Console.WriteLine("--> Inbound post #Command Service successful");
                return Ok("Save was succesful");
            }
            Console.WriteLine("--> Inbound post #Command Service unsuccessful");
            return StatusCode(500, "Inbound test of From Platform Controller failed");
        }

        [HttpGet]
        public async Task<ActionResult> GetPlatformTrackers()
        {
            Console.WriteLine("--> GetPlatformTrackers post #Command Service");
            return Ok(await _repo.GetPlatformTracker());
        }
    }
}