using GiphyWebApi.Models;
using System.Threading.Tasks;

namespace GiphyWebApi.Interfaces
{
    public interface IGiphyTools
    {
        Task<GifResult> GifFetch(string searchTerm);
    }
}
