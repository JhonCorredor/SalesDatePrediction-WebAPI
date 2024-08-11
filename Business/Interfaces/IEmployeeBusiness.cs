using Entity.Dto;
using Entity.Dto.Base;

namespace Business.Interfaces
{
    public interface IEmployeeBusiness
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<EmployeeDTO>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeeDTO> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<EmployeeDTO> Save(EmployeeDTO entityDto);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task Update(EmployeeDTO entityDto);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(int id);
    }
}
