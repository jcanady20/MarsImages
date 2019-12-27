using System;
using System.Collections.Generic;

namespace MarsImages.Internal.Data
{
    public class MemoryCache : IMemoryCache
    {
        private static readonly object _lockobj = new object();
        private readonly List<Models.ImageDate> _dates;
        public MemoryCache()
        {
            _dates = new List<Models.ImageDate>();
        }
        public IReadOnlyList<Models.ImageDate> Dates => _dates;
        public void Add(Models.ImageDate imageDate)
        {
            lock(_lockobj)
            {
                if (_dates.Contains(imageDate)) return;
                _dates.Add(imageDate);
            }
        }
    }
}