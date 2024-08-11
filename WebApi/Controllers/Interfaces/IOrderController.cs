using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface IOrderController
    {
        Task<ActionResult<OrderDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<OrderDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<OrderDTO>> Save(OrderDTO dto);

        Task<ActionResult<OrderDTO>> Update(OrderDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
