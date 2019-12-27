using System.Threading.Tasks;

namespace MarsImages.Internal.Services
{
    public interface IImageService
    {
        Task<string> CacheMarsImageAsync(Data.Models.Photo photoMetaData);
    }
}