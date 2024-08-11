using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.Dto;
using Entity.Dto.Base;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class EmployeeData : IEmployeeData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EmployeeData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"
                SELECT
                    employee.EmpId AS EmpId,
                    employee.LastName,
                    employee.FirstName,
                    CONCAT(employee.FirstName, ' ', employee.LastName) AS FullName,
                    employee.Title,
                    employee.BirthDate,
                    employee.HireDate,
                    employee.City,
                    employee.Country,
                    employee.MgrId
                FROM
                    HR.Employees AS employee
                WHERE employee.MgrId IS NULL "; 

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND employee." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                sql += "AND (UPPER(CONCAT(employee.FirstName, ' ', employee.LastName)) LIKE UPPER(CONCAT('%', @filter, '%'))) ORDER BY " + (filters.ColumnOrder ?? "employee.EmpId") + " " + (filters.DirectionOrder ?? "asc");
            }

            IEnumerable<EmployeeDTO> items = await _context.QueryAsync<EmployeeDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }

        public async Task<Employee> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<Employee>().FindAsync(EmpId);
        }

        public async Task<Employee> Save(Employee entity)
        {
            _context.Set<Employee>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int EmpId)
        {
            int entity = await _context.Set<Employee>().Where(d => d.EmpId == EmpId).ExecuteDeleteAsync();
            return entity;
        }
    }
}
