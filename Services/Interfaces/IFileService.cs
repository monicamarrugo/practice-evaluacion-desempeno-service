using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IFileService
    {
        Task SaveFileAsync(string blobName, string filePath);
        Task<FileDto> GetFileAsync(int  idRecord);
    }
}
