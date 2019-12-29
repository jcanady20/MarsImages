using System;
using System.Threading.Tasks;
using MarsImages.Internal.Data.Models;

namespace MarsImages.Internal.Services
{
    public interface IMarsImageMetaDataHttpClient
    {
        Uri BaseAddress { get; }
        Task<ImageResponse> GetRoverMetaDataByDateAsync(Uri uri);
    }
}