using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface ICustomerController
    {
        Task<ActionResult<CustomerDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<CustomerDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<CustomerDTO>> Save(CustomerDTO dto);

        Task<ActionResult<CustomerDTO>> Update(CustomerDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
