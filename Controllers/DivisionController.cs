using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class DivisionController : ControllerBase
    {
        private IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            this._divisionService = divisionService;

        }

        [HttpGet("listDivisions")]
        public IActionResult GetListDivisions()
        {
            var divisions = this._divisionService.GetAllDivisions();
            return Ok(divisions);
        }


    }
}
