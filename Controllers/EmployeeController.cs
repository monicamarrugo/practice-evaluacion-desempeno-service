using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    [Route("/api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("listEmployees")]
        public IActionResult GetListEmployees()
        {
            var employees = this._employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("listEmployeesWithUsers")]
        public async Task<IActionResult> GetAllEmployeesWithUsers(int pageNumber, int pageSize)
        {
            var employees = await this._employeeService.GetAllEmployeesWithUsers(pageNumber, pageSize);
            return Ok(employees);
        }

        [HttpGet("listEmployeesByResponsible")]
        public IActionResult GetListEmployeesByResponsible(int idResponsible)
        {
            var employees = this._employeeService.GetAllEmployeesByResponsible(idResponsible);
            return Ok(employees);
        }

        [HttpPost("listEmployeesRecord")]
        public async Task<IActionResult> GetEmployeesEvaluations([FromBody]  SearchEmployeesDto dataSearch)
        {
            var employees = await  this._employeeService.GetEmployeesFromRecordsAsync(dataSearch);
            return Ok(employees);
        }

        [HttpPost("listEvaluatorRecords")]
        public async Task<IActionResult> GetEvaluatorRecords([FromBody] SearchEmployeesDto dataSearch)
        {
            var employees = await this._employeeService.GetEvaluatorRecords(dataSearch);
            return Ok(employees);
        }

        [HttpPost("saveEmployee")]
        public IActionResult SaveEmployee([FromBody] EmployeeDto employeeData)
        {
            var respuestaInscripcion = this._employeeService.SaveEmployee(employeeData);
            return Ok(respuestaInscripcion);
        }

        [HttpPost("updateEmployee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeData)
        {
            var respuestaInscripcion = this._employeeService.UpdatelEmployee(employeeData);
            return Ok(respuestaInscripcion);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var question = this._employeeService.GetById(id);
            return Ok(question);
        }
    }
}
