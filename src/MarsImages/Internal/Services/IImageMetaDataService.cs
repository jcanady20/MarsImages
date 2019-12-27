using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarsImages.Internal.Data.Models;

namespace MarsImages.Internal.Services
{
    public interface IImageMetaDataService
    {
         Task<IReadOnlyList<Photo>> GetMetaDataByDateAsync(DateTime date, int page = 1, string roverName = "curiosity");
    }
}