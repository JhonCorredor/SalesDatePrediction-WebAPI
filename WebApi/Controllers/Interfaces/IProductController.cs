using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface IProductController
    {
        Task<ActionResult<ProductDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<ProductDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<ProductDTO>> Save(ProductDTO dto);

        Task<ActionResult<ProductDTO>> Update(ProductDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
