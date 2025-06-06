using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IQuestionaryRepository
    {

        List<Questionaries> GetAllIncludes();
        List<Questionaries> GetByTypeIncludes(string type);
        public Questionaries GetByIdIncludes(int id);
    }
}
