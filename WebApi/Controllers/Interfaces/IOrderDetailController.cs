using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface IOrderDetailController
    {
        Task<ActionResult<OrderDetailDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<OrderDetailDTO>> Save(OrderDetailDTO dto);

        Task<ActionResult<OrderDetailDTO>> Update(OrderDetailDTO dto);

        Task<ActionResult> Delete(int idOrden, int idProducto);
    }
}
