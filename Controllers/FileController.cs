using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using EvaluacionDesempenoApi.Services;
using EvaluacionDesempenoApi.Services.Interfaces;
using System.Text.Json;
using EvaluacionDesempenoApi.DTOs;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IDynamicBlobService _blobService;
        private readonly IFileService _fileService;

        public FileController(IDynamicBlobService blobService, IFileService fileService)
        {
            _blobService = blobService;
            _fileService = fileService;
        }
        [HttpGet("getFileByRecord")]
        public async Task<IActionResult> GetFileByRecord([FromQuery] int idRecord)
        {
            var file = await this._fileService.GetFileAsync(idRecord);
            return Ok(file);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob(IFormFile file, [FromForm] string dto)
        {
            var fileDto = JsonSerializer.Deserialize<FileDto>(dto);
            ResponseTransaction responseCreate = new ResponseTransaction();

            if (file != null && file.Length > 0 && fileDto != null)
            {
                responseCreate = await _blobService.UploadBlobAsync(fileDto, file);
            }
           
            return Ok(responseCreate);
        }

        [HttpGet("list/{year}")]
        public async Task<IActionResult> ListBlobs(int year)
        {
            var blobs = await _blobService.ListBlobsAsync(year);
            return Ok(blobs);
        }

        [HttpDelete("delete/{year}/{blobName}")]
        public async Task<IActionResult> DeleteBlob(int year, string blobName)
        {
            await _blobService.DeleteBlobAsync(year, blobName);
            return Ok("Blob deleted successfully.");
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadBlob([FromQuery] string blobName)
        {
            var stream = await _blobService.DownloadBlobAsync(blobName);
            if (stream == null)
            {
                return NotFound();
            }

            // Configura el tipo de contenido adecuado según el tipo de archivo
            var contentType = "application/pdf"; // Usa un tipo de contenido específico si es conocido
            var fileName = blobName; // El nombre del archivo que se descargará

            return File(stream, contentType, fileName);
        }
    }
}
