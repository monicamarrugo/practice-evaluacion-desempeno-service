using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {

        private IFlagService _flagService;
        private IColorService _colorService;
        private IFlagTypeService _flagTypeService;

        public FlagController(IFlagService flagService, IColorService colorService, IFlagTypeService flagTypeService)
        {
            _flagService = flagService;
            _colorService = colorService;
            _flagTypeService = flagTypeService;
        }

        [HttpPost("saveFlag")]
        public IActionResult SaveFlag([FromBody] FlagsDto flagData)
        {
            var responseCreate = this._flagService.SaveFlag(flagData);
            return Ok(responseCreate);
        }

        [HttpGet("listFlags")]
        public IActionResult GetFlags()
        {
            var flags = this._flagService.GetFlags();
            return Ok(flags);
        }

        [HttpGet("getFlagById")]
        public IActionResult GetFlagById([FromQuery] int id)
        {
            var flag = this._flagService.GetFlagById(id);
            return Ok(flag);
        }

        [HttpGet("getColors")]
        public IActionResult GetColors()
        {
            var colors = this._colorService.GetColors();
            return Ok(colors);
        }
        [HttpGet("getFlagTypes")]
        public IActionResult GetFlagTypes()
        {
            var types = this._flagTypeService.GetFlagTypes();
            return Ok(types);
        }
    }
}
