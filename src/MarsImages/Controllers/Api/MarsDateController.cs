using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarsImages.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarsDateController : ControllerBase
    {
        private readonly Internal.Data.IMemoryCache _memoryCache;
        public MarsDateController(Internal.Data.IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get()
        {
            if (_memoryCache == null) return NotFound();
            if (_memoryCache.Dates == null || _memoryCache.Dates.Count == 0) return NotFound();
            return Ok(_memoryCache.Dates.Select(r => new { r.Name, r.Date, r.Status, r.Photos }));
        }
    }
}