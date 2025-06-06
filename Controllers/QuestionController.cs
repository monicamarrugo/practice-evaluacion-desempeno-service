using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private IQuestionTypeService _questionTypeService;
        private IQuestionService _questionService;

        public QuestionController(IQuestionTypeService questionTypeService, IQuestionService questionService)
        {
            this._questionTypeService = questionTypeService;
            this._questionService = questionService;
        }

        [HttpGet("listQuestionTypes")]
        public IActionResult GetListTipoPreguntas()
        {
            var tipos = this._questionTypeService.GetAllTipoPregunta();
            return Ok(tipos);
        }

        [HttpGet("listQuestions")]
        public IActionResult GetListQuestion()
        {
            var tipos = this._questionService.GetAllQuestions();
            return Ok(tipos);
        }

        [HttpGet("listQuestionsByType")]
        public IActionResult GetListQuestionByType(string questionType)
        {
            var tipos = this._questionService.GetQuestionsByType(questionType);
            return Ok(tipos);
        }

        [HttpGet("listQuestionsByArea")]
        public IActionResult GetListQuestionByArea(string area)
        {
            var tipos = this._questionService.GetQuestionsByArea(area);
            return Ok(tipos);
        }

        [HttpGet("listQuestionsByGroup")]
        public IActionResult GetListQuestionByGroup(int group)
        {
            var tipos = this._questionService.GetQuestionsByGroup(group);
            return Ok(tipos);
        }

        [HttpPost("saveQuestion")]
        public IActionResult SaveQuestion([FromBody] QuestionDto questionData)
        {
            var respuestaInscripcion = this._questionService.SaveQuestion(questionData);
            return Ok(respuestaInscripcion);
        }

        [HttpPost("updateQuestion")]
        public IActionResult UpdateQuestion([FromBody] QuestionDto questionData)
        {
            var respuestaInscripcion = this._questionService.UpdateQuestion(questionData);
            return Ok(respuestaInscripcion);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var question = this._questionService.GetById(id);
            return Ok(question);
        }

        [HttpGet("listFormats")]
        public IActionResult GetListFormats()
        {
            var tipos = this._questionService.GetAllFormats();
            return Ok(tipos);
        }

    }
}
