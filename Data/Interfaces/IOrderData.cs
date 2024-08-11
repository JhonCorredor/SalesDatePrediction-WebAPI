using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

namespace Data.Interfaces
{
    public interface IOrderData
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Order> GetById(int EmpId);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Order> Save(Order entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(Order entity);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        Task<int> Delete(int EmpId);
    }
}
