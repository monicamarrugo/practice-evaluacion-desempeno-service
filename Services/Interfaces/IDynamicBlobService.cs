using Azure.Storage.Blobs;
using EvaluacionDesempenoApi.Services.DTOs;
using System.Threading.Tasks;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IDynamicBlobService
    {
        Task<ResponseTransaction> UploadBlobAsync(FileDto fileData, IFormFile file);

        Task<List<string>> ListBlobsAsync(int year);
        Task DeleteBlobAsync(int year, string blobName);

        Task<Stream> DownloadBlobAsync(string blobName);
    }
}
