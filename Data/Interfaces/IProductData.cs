using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

namespace Data.Interfaces
{
    public interface IProductData
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetById(int EmpId);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Product> Save(Product entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(Product entity);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        Task<int> Delete(int EmpId);
    }
}
