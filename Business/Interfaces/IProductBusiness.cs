using Entity.Dto;
using Entity.Dto.Base;

namespace Business.Interfaces
{
    public interface IProductBusiness
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<ProductDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDTO> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<ProductDTO> Save(ProductDTO entityDto);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task Update(ProductDTO entityDto);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(int id);
    }
}
