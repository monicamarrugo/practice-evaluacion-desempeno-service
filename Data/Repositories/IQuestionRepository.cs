using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IQuestionRepository
    {
        List<Questions> GetAllIncludes();
        public Questions GetByIdIncludes(int id);
    }
}
