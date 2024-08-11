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
    public class ShipperData : IShipperData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ShipperData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"SELECT
                    [Sales].[Shippers].ShipperId,
                    [Sales].[Shippers].CompanyName,
                    [Sales].[Shippers].Phone
                FROM
                    [Sales].[Shippers] "; 

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND [Sales].[Shippers]." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {

                if (!string.IsNullOrEmpty(filters.ColumnFilter))
                {
                    sql += @"WHERE [Sales].[Shippers]." + filters.ColumnFilter + " = @filter ";
                }
            }

            sql += "ORDER BY " + (filters.ColumnOrder ?? "[Sales].[Shippers].ShipperId") + " " + (filters.DirectionOrder ?? "asc");

            IEnumerable<ShipperDTO> items = await _context.QueryAsync<ShipperDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }


        public async Task<Shipper> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<Shipper>().FindAsync(EmpId);
        }

        public async Task<Shipper> Save(Shipper entity)
        {
            _context.Set<Shipper>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Shipper entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            int entity = await _context.Set<Shipper>().Where(d => d.ShipperId == id).ExecuteDeleteAsync();
            return entity;
        }
    }
}
