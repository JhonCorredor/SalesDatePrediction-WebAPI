using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface ISupplierController
    {
        Task<ActionResult<SupplierDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<SupplierDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<SupplierDTO>> Save(SupplierDTO dto);

        Task<ActionResult<SupplierDTO>> Update(SupplierDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
