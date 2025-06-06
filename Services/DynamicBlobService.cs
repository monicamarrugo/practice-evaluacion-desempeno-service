using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Enums;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EvaluacionDesempenoApi.Services
{
    public class DynamicBlobService: IDynamicBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IGenericRepository<Files> _filesGenericRepository;
        private readonly IMapper _mapper;

        public DynamicBlobService(BlobServiceClient blobServiceClient,
            IGenericRepository<Files> filesGenericRepository,
            IMapper mapper
            )
        {
            _blobServiceClient = blobServiceClient;
            _filesGenericRepository = filesGenericRepository;
            _mapper = mapper;
        }
        private BlobContainerClient GetBlobContainerClient(string containerName)
        {
            
            // Obtener el cliente del contenedor
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            // Crear el contenedor si no existe
            containerClient.CreateIfNotExists();
            return containerClient;
        }
        public async Task<ResponseTransaction> UploadBlobAsync(FileDto fileData, IFormFile file)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                
                // Generar el nombre del contenedor basado en el año
                var containerName = $"{(fileData.cdFileType == FileTypeEnum.EvaluationFile.GetStringValue()? "evaluaciones":"kpi")}";
                var containerClient = GetBlobContainerClient(containerName);
                var blobname = $"{fileData.year}/{fileData.nameFile}";
                var blobClient = containerClient.GetBlobClient(blobname);
                await using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/pdf" });
                stream.Close();
                fileData.nameFile = blobname;
                var entity = _mapper.Map<Files>(fileData);
                await _filesGenericRepository.AddAsync(entity);
                response.error = "NO";
                response.message = "Archivo guardado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public async Task<List<string>> ListBlobsAsync(int year)
        {
            var containerName = $"evaluaciones-{year}";
            var containerClient = GetBlobContainerClient(containerName);
            var blobs = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }
            return blobs;
        }
        public async Task DeleteBlobAsync(int year, string blobName)
        {
            var containerName = $"evaluaciones-{year}";
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var containerName = $"evaluaciones";
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return blobDownloadInfo.Value.Content;
        }

    }
}
