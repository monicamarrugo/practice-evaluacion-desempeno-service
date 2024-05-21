using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class AreasController : ControllerBase
    {
        private IAreaService _areasService;

        public AreasController(IAreaService areasService)
        {
            this._areasService = areasService;

        }

        [HttpGet("listAreas")]
        public IActionResult GetListAreas()
        {
            var areas = this._areasService.GetAllAreas();
            return Ok(areas);
        }
    }
}
