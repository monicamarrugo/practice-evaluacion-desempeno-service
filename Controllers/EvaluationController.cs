using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private IEvaluationService _evaluationService;
        private IEscaleService _scaleService;
        private IDivisionService _divisionService;

        public EvaluationController(IEvaluationService evaluationService, IEscaleService scaleService, IDivisionService divisionService)
        {
            _evaluationService = evaluationService;
            _scaleService = scaleService;
            _divisionService = divisionService;
        }

        [HttpGet("listEvaluations")]
        public IActionResult GetListEvaluations()
        {
            var evaluations = this._evaluationService.GetAllEvaluations();
            return Ok(evaluations);
        }

        [HttpPost("listActiveEvaluations")]
        public async Task<IActionResult> GetListActiveEvaluations([FromBody] SearchActiveEvaluationDto dataSearch)
        {
            var evaluations = await this._evaluationService.GetActiveEvaluation(dataSearch);
            return Ok(evaluations);
        }

        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var evaluation = this._evaluationService.GetEvaluationsById(id);
            return Ok(evaluation);
        }

        [HttpGet("getByIdInclude")]
        public IActionResult GetByIdInclude([FromQuery] int id)
        {
            var evaluation = this._evaluationService.GetEvaluationsByIdInclude(id);
            return Ok(evaluation);
        }

        [HttpPost("saveEvaluation")]
        public IActionResult SaveEvaluation([FromBody] EvaluationCreateDto evaluationData)
        {
            var responseCreate = this._evaluationService.SaveEvaluation(evaluationData);
            return Ok(responseCreate);
        }

        [HttpPost("updateEvaluation")]
        public IActionResult UpdateEvaluation([FromBody] EvaluationCreateDto evaluationData)
        {
            var responseUpdate = this._evaluationService.UpdateEvaluation(evaluationData);
            return Ok(responseUpdate);
        }

        [HttpPost("enableEvaluation")]
        public IActionResult EnableEvaluation([FromBody] EvaluationCreateDto evaluationData)
        {
            var responseUpdate = this._evaluationService.EnableEvaluation(evaluationData);
            return Ok(responseUpdate);
        }

        [HttpGet("getScales")]
        public IActionResult GetScales()
        {
            var scales = this._scaleService.GetAllEscales();
            return Ok(scales);
        }

        [HttpGet("getDivisions")]
        public IActionResult GetDivisions()
        {
            var divisions = this._divisionService.GetAllDivisions();
            return Ok(divisions);
        }
    }
}
