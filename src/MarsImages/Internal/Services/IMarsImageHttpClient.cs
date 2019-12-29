using System.IO;
using System.Threading.Tasks;

namespace MarsImages.Internal.Services
{
    public interface IMarsImageHttpClient
    {
        Task<Stream> CacheMarsImageAsync(Data.Models.Photo photoMetaData);
    }
}