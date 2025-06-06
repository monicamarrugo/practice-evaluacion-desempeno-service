using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IFileRepository
    {
        public Task<Files> GetByIdRecordAsync(int idRecord);
    }
}
