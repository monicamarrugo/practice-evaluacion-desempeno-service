using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class FileService: IFileService
    {
        private readonly IGenericRepository<Files> _filesGenericRepository;
        private readonly IFileRepository _filesRepository;
        private readonly IMapper _mapper;

        public FileService(IGenericRepository<Files> filesGenericRepository,
            IFileRepository filesRepository,
            IMapper mapper)
        {
            _filesGenericRepository = filesGenericRepository;
            _filesRepository = filesRepository;
            _mapper = mapper;
        }

        public async Task<FileDto> GetFileAsync(int idRecord)
        {
            try
            {
                FileDto fileDto = new FileDto();
                var entity = await _filesRepository.GetByIdRecordAsync(idRecord);
                if (entity != null)
                {
                    fileDto = _mapper.Map<FileDto>(entity);
                }
                
                return fileDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task SaveFileAsync(string blobName, string filePath)
        {
            // Aquí va el código para insertar los datos en la base de datos
            // Por ejemplo, usando Dapper, Entity Framework, ADO.NET, etc.
            // await dbContext.BlobRecords.AddAsync(new BlobRecord { BlobName = blobName, FilePath = filePath });
            // await dbContext.SaveChangesAsync();
        }
    }
}
