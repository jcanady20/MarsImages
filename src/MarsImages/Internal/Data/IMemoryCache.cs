using System.Collections.Generic;

namespace MarsImages.Internal.Data
{
    public interface IMemoryCache
    {
         IReadOnlyList<Models.ImageDate> Dates { get; }
         void Add(Models.ImageDate imageDate);
    }
}