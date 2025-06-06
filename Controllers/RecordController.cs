using EvaluacionDesempenoApi.DTOs;
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
        public async Task<IActionResult> GetRecordTempById([FromQuery] int id)
        {
            var evaluation = await this._recordService.GetRecordTempById(id);
            return Ok(evaluation);
        }

        [HttpGet("getRecordById")]
        public async Task<IActionResult> GetRecordById([FromQuery] int id)
        {
            var evaluation = await this._recordService.GetRecordById(id);
            return Ok(evaluation);
        }

        [HttpPost("listEvaluatorRecordsByParams")]
        public async Task<IActionResult> GetEvaluatorRecords([FromBody] SearchEmployeesDto dataSearch)
        {
            var employees = await this._recordService.GetEvaluatorRecordsByParams(dataSearch);
            return Ok(employees);
        }
    }
}
