using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionService
    {
        public List<QuestionDto> GetAllQuestions();
        public List<FormatDto> GetAllFormats();
        public ResponseTransaction SaveQuestion(QuestionDto question);
        public ResponseTransaction UpdateQuestion(QuestionDto question);
        public void DisableQuestion();
        public QuestionDto GetById(int id);
    }
}
