using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;

public class EmployeeBusiness : IEmployeeBusiness
{
    private readonly IEmployeeData _data;
    private readonly IMapper _mapper;

    public EmployeeBusiness(IEmployeeData data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    public async Task<EmployeeDTO> Save(EmployeeDTO dto)
    {
        IEnumerable<EmployeeDTO> existingEmployees = await _data.GetDataTable(new QueryFilterDto
        {
            Filter = $"{dto.FirstName} {dto.LastName}",
            NameForeignKey = "FirstName" 
        });

        if (existingEmployees.Any())
        {
            throw new Exception("¡Ya existe un empleado con el mismo nombre completo!");
        }

        dto.HireDate = DateTime.UtcNow;

        Employee entity = _mapper.Map<Employee>(dto);
        entity = await _data.Save(entity);
        return _mapper.Map<EmployeeDTO>(entity);
    }

    public async Task Update(EmployeeDTO dto)
    {
        IEnumerable<EmployeeDTO> existingEmployees = await _data.GetDataTable(new QueryFilterDto
        {
            Filter = $"{dto.FirstName} {dto.LastName}",
            NameForeignKey = "FirstName"
        });

        if (existingEmployees.Any(e => e.EmpId != dto.EmpId))
        {
            throw new Exception("¡Ya existe un empleado con el mismo nombre completo!");
        }

        dto.HireDate = DateTime.UtcNow;
        Employee entity = _mapper.Map<Employee>(dto);
        await _data.Update(entity);
    }

    public async Task<List<EmployeeDTO>> GetDataTable(QueryFilterDto filters)
    {
        IEnumerable<EmployeeDTO> employees = await _data.GetDataTable(filters);
        return employees.ToList();
    }

    public async Task<EmployeeDTO> GetById(int id)
    {
        Employee entity = await _data.GetById(id);
        return _mapper.Map<EmployeeDTO>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await _data.Delete(id);
    }
}
