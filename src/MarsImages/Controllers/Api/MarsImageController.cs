using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsImages.Internal.Extensions;
using MarsImages.Internal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarsImages.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarsImageController : ControllerBase
    {
        private readonly Internal.Data.IMemoryCache _memoryCache;
        public MarsImageController(Internal.Data.IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_memoryCache == null || _memoryCache.Dates == null || _memoryCache.Dates.Count == 0) return NotFound();
            var photo = _memoryCache
                .Dates.SelectMany(x => x.Photos)
                .FirstOrDefault(x => x.Id == id);
            if (photo == null) return NotFound();
            if (string.IsNullOrEmpty(photo.LocalSource)) return NotFound();
            var image = System.IO.File.OpenRead(photo.LocalSource);
            return File(image, "image/jpeg");
        }

    }
}