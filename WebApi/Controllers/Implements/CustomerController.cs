using Business.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Interfaces;

namespace WebApi.Controllers.Implementations
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase, ICustomerController
    {
        private readonly ICustomerBusiness _business;

        public CustomerController(ICustomerBusiness business)
        {
            _business = business;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            try
            {
                var data = await _business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<CustomerDTO>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<CustomerDTO>(data, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<CustomerDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("datatable")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetDataTable([FromQuery] QueryFilterDto filters)
        {
            try
            {
                var lstDto = await _business.GetDataTable(filters);
                var response = new ApiResponse<IEnumerable<CustomerDTO>>(lstDto, true, "Ok", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<CustomerDTO>>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("getSalesDatePrediction")]
        public async Task<ActionResult<IEnumerable<SalesDatePredictionDto>>> GetSalesDatePrediction([FromQuery] QueryFilterDto filters)
        {
            try
            {
                var lstDto = await _business.GetSalesDatePrediction(filters);
                var response = new ApiResponse<IEnumerable<SalesDatePredictionDto>>(lstDto, true, "Ok", null!);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<SalesDatePredictionDto>>(null!, false, ex.Message, null!);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Save(CustomerDTO dto)
        {
            try
            {
                var dtoSaved = await _business.Save(dto);
                var response = new ApiResponse<CustomerDTO>(dtoSaved, true, "Registro almacenado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dtoSaved.CustId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<CustomerDTO>(null, false, ex.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> Update(CustomerDTO dto)
        {
            try
            {
                await _business.Update(dto);
                var response = new ApiResponse<CustomerDTO>(dto, true, "Registro actualizado exitosamente", null);
                return new CreatedAtRouteResult(new { id = dto.CustId }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<CustomerDTO>(null, false, ex.Message, null);
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
                    var errorResponse = new ApiResponse<CustomerDTO>(null, false, "Registro no eliminado", null);
                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }
                var successResponse = new ApiResponse<CustomerDTO>(null, true, "Registro eliminado exitosamente", null);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<CustomerDTO>(null, false, "El registro se encuentra asociado, no se puede eliminar", null);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
