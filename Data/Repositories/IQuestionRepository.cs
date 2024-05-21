using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IQuestionRepository
    {
        List<Questions> GetAllIncludes();
        public Questions GetByIdIncludes(int id);
        public List<Questions> GetByType(string questionType);
        public List<Questions> GetByGroup(int idGroup);
        public List<Questions> GetByArea(string area);
    }
}
