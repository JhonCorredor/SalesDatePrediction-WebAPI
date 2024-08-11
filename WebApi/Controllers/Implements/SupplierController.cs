using Business.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Interfaces;

namespace WebApi.Controllers.Implementations
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase, ISupplierController
    {
        private readonly ISupplierBusiness _business;

        public SupplierController(ISupplierBusiness business)
        {
            _business = business;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetById(int id)
        {
            try
            {
                var data = await _business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<SupplierDTO>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<SupplierDTO>(data, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<SupplierDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("datatable")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetDataTable([FromQuery] QueryFilterDto filters)
        {
            try
            {
                var lstDto = await _business.GetDataTable(filters);
                var response = new ApiResponse<IEnumerable<SupplierDTO>>(lstDto, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<SupplierDTO>>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Save(SupplierDTO dto)
        {
            try
            {
                var dtoSaved = await _business.Save(dto);
                var response = new ApiResponse<SupplierDTO>(dtoSaved, true, "Registro almacenado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dtoSaved.SupplierId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<SupplierDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<SupplierDTO>> Update(SupplierDTO dto)
        {
            try
            {
                await _business.Update(dto);
                var response = new ApiResponse<SupplierDTO>(dto, true, "Registro actualizado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dto.SupplierId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<SupplierDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                int registroAfectados = await _business.Delete(id);
                if (registroAfectados == 0)
                {
                    var errorResponse = new ApiResponse<SupplierDTO>(null, false, "Registro no eliminado", null);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }
                var successResponse = new ApiResponse<SupplierDTO>(null, true, "Registro eliminado exitosamente", null);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<SupplierDTO>(null, false, "El registro se encuentra asociado, no se puede eliminar", null);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
