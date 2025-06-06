namespace EvaluacionDesempenoApi.DTOs
{
    public class CreateQuestionaryConfigDto
    {
        public QuestionaryDto questionary { get; set; }
        public List<QuestionariesConfigDto> questionariesConfig { get; set; }
    }
}
