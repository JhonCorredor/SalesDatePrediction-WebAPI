using Entity.Dto;
using Entity.Dto.Base;

namespace Business.Interfaces
{
    public interface ISupplierBusiness
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<SupplierDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SupplierDTO> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<SupplierDTO> Save(SupplierDTO entityDto);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task Update(SupplierDTO entityDto);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(int id);
    }
}
