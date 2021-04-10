using GiphyWebApi.Models;
using System;
using System.Threading.Tasks;

namespace GiphyWebApi.Interfaces
{
    public interface IWebTools
    {
        Task<WebResult> GetData(Uri uri);
    }
}
