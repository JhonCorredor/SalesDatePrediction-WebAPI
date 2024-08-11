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
    public class SupplierData : ISupplierData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SupplierData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"SELECT
                    s.SupplierId,
                    s.CompanyName,
                    s.ContactName,
                    s.ContactTitle,
                    s.Address,
                    s.City,
                    s.Region,
                    s.PostalCode,
                    s.Country,
                    s.Phone,
                    s.Fax
                FROM
                    Suppliers AS s";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND s." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                sql += "AND (UPPER(s.CompanyName) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(s.ContactName) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(s.ContactTitle) LIKE UPPER(CONCAT('%', @filter, '%'))) " +
                       "ORDER BY " + (filters.ColumnOrder ?? "s.SupplierId") + " " + (filters.DirectionOrder ?? "asc");
            }

            IEnumerable<SupplierDTO> items = await _context.QueryAsync<SupplierDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }

        public async Task<Supplier> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<Supplier>().FindAsync(EmpId);
        }

        public async Task<Supplier> Save(Supplier entity)
        {
            _context.Set<Supplier>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Supplier entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int supplierId)
        {
            int entity = await _context.Set<Supplier>().Where(d => d.SupplierId == supplierId).ExecuteDeleteAsync();
            return entity;
        }
    }
}
