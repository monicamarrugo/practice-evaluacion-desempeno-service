using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class GroupController : ControllerBase
    {
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            this._groupService = groupService;

        }

        [HttpGet("listGroups")]
        public IActionResult GetListGroup()
        {
            var groups = this._groupService.GetAllGroups();
            return Ok(groups);
        }


    }
}
