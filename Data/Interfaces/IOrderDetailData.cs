using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

namespace Data.Interfaces
{
    public interface IOrderDetailData
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDetailDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderDetail> GetById(int EmpId);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<OrderDetail> Save(OrderDetail entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(OrderDetail entity);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        Task<int> Delete(int idOrden, int idProducto);
    }
}
