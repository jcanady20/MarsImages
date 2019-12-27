using System.Threading.Tasks;

namespace MarsImages.Internal.Services
{
    public interface IPhotoCacheService
    {
        void InitializeCache();
        Task PreFetchImageMetaDataAsync();
        Task CacheImagesAsync();
    }
}