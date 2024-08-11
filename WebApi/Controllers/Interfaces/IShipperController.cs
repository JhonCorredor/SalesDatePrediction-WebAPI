using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface IShipperController
    {
        Task<ActionResult<ShipperDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<ShipperDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<ShipperDTO>> Save(ShipperDTO dto);

        Task<ActionResult<ShipperDTO>> Update(ShipperDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
