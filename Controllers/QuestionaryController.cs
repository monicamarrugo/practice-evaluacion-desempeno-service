using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class QuestionaryController : ControllerBase
    {
        private IQuestionaryTypeService _questionaryTypeService;
        private IQuestionariesConfigService _questionaryConfigService;
        private IQuestionaryService _questionaryService;

        public QuestionaryController(IQuestionaryTypeService questionaryTypeService, 
            IQuestionariesConfigService questionaryConfigService,
            IQuestionaryService questionaryService)
        {
            this._questionaryTypeService = questionaryTypeService;
            _questionaryConfigService = questionaryConfigService;
            _questionaryService = questionaryService;
        }

        [HttpGet("listQuestionaryTypes")]
        public IActionResult GetListQuestionaryTypes()
        {
            var types = this._questionaryTypeService.GetAllQuestionaryTypes();
            return Ok(types);
        }

        [HttpGet("configQuestionaryByIdQuestionary")]
        public async Task<IActionResult>  GetConfigByQuestionary(int id)
        {
            var types = await this._questionaryConfigService.GetByQuestionary(id);
            return Ok(types);
        }

        [HttpGet("configQuestionaryToRecord")]
        public async Task<IActionResult> GetConfigToRecord(int id)
        {
            var types =  await this._questionaryConfigService.GetQuestionaryToRecord(id);
            return Ok(types);
        }

        [HttpGet("configCompleteByIdQuestionary")]
        public IActionResult GetConfigCompleteByQuestionary(int id)
        {
            var types = this._questionaryConfigService.GetCompleteByQuestionary(id);
            return Ok(types);
        }

        [HttpGet("listQuestionaries")]
        public IActionResult GetListQUestionaries()
        {
            var questioaries = this._questionaryService.GetAllQuestionaries();
            return Ok(questioaries);
        }

        [HttpGet("listQuestionaryByType")]
        public IActionResult GetListByType(string type)
        {
            var questioaries = this._questionaryService.GetAllQuestionariesByType(type);
            return Ok(questioaries);
        }
        [HttpPost("saveQuestionary")]
        public IActionResult SaveQuestionary([FromBody] CreateQuestionaryConfigDto questionaryData)
        {
            var respuesta = this._questionaryService.SaveQuestionary(questionaryData);
            return Ok(respuesta);
        }

        [HttpPost("updateQuestionary")]
        public IActionResult UpdateQuestionary([FromBody] CreateQuestionaryConfigDto questionaryData)
        {
            var respuesta = this._questionaryService.UpdateQuestionary(questionaryData);
            return Ok(respuesta);
        }
    }
}
