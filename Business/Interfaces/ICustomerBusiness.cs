using Entity.Dto;
using Entity.Dto.Base;

namespace Business.Interfaces
{
    public interface ICustomerBusiness
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<CustomerDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// GetSalesDatePrediction
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<SalesDatePredictionDto>> GetSalesDatePrediction(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerDTO> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<CustomerDTO> Save(CustomerDTO entityDto);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task Update(CustomerDTO entityDto);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(int id);
    }
}
