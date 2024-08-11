using Entity.Dto;
using Entity.Dto.Base;

namespace Business.Interfaces
{
    public interface IOrderDetailBusiness
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<OrderDetailDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderDetailDTO> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<OrderDetailDTO> Save(OrderDetailDTO entityDto);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task Update(OrderDetailDTO entityDto);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(int idOrden, int idProducto);
    }
}
