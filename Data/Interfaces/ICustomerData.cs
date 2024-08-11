using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

namespace Data.Interfaces
{
    public interface ICustomerData
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<CustomerDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// GetSalesDatePrediction
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<SalesDatePredictionDto>> GetSalesDatePrediction(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> GetById(int EmpId);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Customer> Save(Customer entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(Customer entity);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        Task<int> Delete(int EmpId);
    }
}
