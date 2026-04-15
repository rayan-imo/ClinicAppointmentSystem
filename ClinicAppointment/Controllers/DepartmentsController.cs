using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService _departmentService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<DepartmentDtos>> GetAll([FromQuery] PaginationParameter pagination)
        {
            var result = await _departmentService.GetAllAsync(pagination);

            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<ActionResult<DepartmentDtos>> GetByIdAsync(Guid id)
        {
            var departments = await _departmentService.GetByIdAsync(id);
            if (departments is null)
            {
                return NotFound($"Department with ID {id} not found");
            }
            return Ok(departments);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDtos>> AddAsync(DepartmentDto dto)
        {
            var department = await _departmentService.AddAsync(dto);
            return Ok(department);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDtos>> UpdateAsync(Guid id, DepartmentDto dto)
        {
            var departmentId = await _departmentService.UpdateAsync(id, dto);
            if (departmentId == Guid.Empty)
            {
                return NotFound($"Doctor with ID {id} not found");
            }
            return Ok(departmentId);
        }
        [HttpPut("soft/{id}")]
        public async Task<ActionResult> SoftDeleteAsync(Guid id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound($"Department with ID {id} not found");

            await _departmentService.SoftDeleteAsync(id);
            return Ok();
        }
        [HttpDelete("hard/{id}")]
        public async Task<ActionResult> HardDeleteAsync(Guid id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound($"Doctor with ID {id} not found");

            await _departmentService.HardDeleteAsync(id);
            return Ok();
        }
    }

}

