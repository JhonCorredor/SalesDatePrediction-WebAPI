using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface IEmployeeController
    {
        Task<ActionResult<EmployeeDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<EmployeeDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<EmployeeDTO>> Save(EmployeeDTO dto);

        Task<ActionResult<EmployeeDTO>> Update(EmployeeDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
