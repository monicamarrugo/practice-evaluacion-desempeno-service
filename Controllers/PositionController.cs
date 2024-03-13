using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class PositionController : ControllerBase
    {
        private IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            this._positionService = positionService;

        }

        [HttpGet("listPositions")]
        public IActionResult GetListPositions()
        {
            var positions = this._positionService.GetAllPositions();
            return Ok(positions);
        }


    }
}
