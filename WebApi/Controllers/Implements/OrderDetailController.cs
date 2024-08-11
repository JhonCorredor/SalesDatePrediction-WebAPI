using Business.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Interfaces;

namespace WebApi.Controllers.Implementations
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase, IOrderDetailController
    {
        private readonly IOrderDetailBusiness _business;

        public OrderDetailController(IOrderDetailBusiness business)
        {
            _business = business;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDTO>> GetById(int id)
        {
            try
            {
                var data = await _business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<OrderDetailDTO>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<OrderDetailDTO>(data, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<OrderDetailDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("datatable")]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetDataTable([FromQuery] QueryFilterDto filters)
        {
            try
            {
                var lstDto = await _business.GetDataTable(filters);
                var response = new ApiResponse<IEnumerable<OrderDetailDTO>>(lstDto, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<OrderDetailDTO>>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailDTO>> Save(OrderDetailDTO dto)
        {
            try
            {
                var dtoSaved = await _business.Save(dto);
                var response = new ApiResponse<OrderDetailDTO>(dtoSaved, true, "Registro almacenado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dtoSaved.OrderId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<OrderDetailDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<OrderDetailDTO>> Update(OrderDetailDTO dto)
        {
            try
            {
                await _business.Update(dto);
                var response = new ApiResponse<OrderDetailDTO>(dto, true, "Registro actualizado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dto.OrderId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<OrderDetailDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int idOrden, int idProducto)
        {
            try
            {
                int registroAfectados = await _business.Delete(idOrden, idProducto);
                if (registroAfectados == 0)
                {
                    var errorResponse = new ApiResponse<OrderDetailDTO>(null, false, "Registro no eliminado", null);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }
                var successResponse = new ApiResponse<OrderDetailDTO>(null, true, "Registro eliminado exitosamente", null);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<OrderDetailDTO>(null, false, "El registro se encuentra asociado, no se puede eliminar", null);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
