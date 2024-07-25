using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class RecordController : ControllerBase
    {
        private IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost("saveRecordEvaluation")]
        public IActionResult SaveEvaluation([FromBody] CreateEvaluationRecordDto recordData)
        {
            var responseCreate = this._recordService.SaveRecordEvaluation(recordData);
            return Ok(responseCreate);
        }

        [HttpPost("finishEvaluation")]
        public IActionResult FinishEvaluation([FromBody] CreateEvaluationRecordDto recordData)
        {
            var responseCreate = this._recordService.FinishEvaluation(recordData);
            return Ok(responseCreate);
        }

        [HttpPost("updateRecordEvaluation")]
        public IActionResult UpdateEvaluation([FromBody] CreateEvaluationRecordDto recordData)
        {
            var responseCreate = this._recordService.UpdateRecordEvaluation(recordData);
            return Ok(responseCreate);
        }

        [HttpGet("getRecordTempById")]
        public IActionResult GetRecordTempById([FromQuery] int id)
        {
            var evaluation = this._recordService.GetRecordTempById(id);
            return Ok(evaluation);
        }

        [HttpGet("getRecordById")]
        public IActionResult GetRecordById([FromQuery] int id)
        {
            var evaluation = this._recordService.GetRecordById(id);
            return Ok(evaluation);
        }
    }
}
