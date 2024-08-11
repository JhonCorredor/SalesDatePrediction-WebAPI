using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Interfaces
{
    public interface ICategoryController
    {
        Task<ActionResult<CategoryDTO>> GetById(int id);

        Task<ActionResult<IEnumerable<CategoryDTO>>> GetDataTable([FromQuery] QueryFilterDto filters);

        Task<ActionResult<CategoryDTO>> Save(CategoryDTO dto);

        Task<ActionResult<CategoryDTO>> Update(CategoryDTO dto);

        Task<ActionResult> Delete(int id);
    }
}
