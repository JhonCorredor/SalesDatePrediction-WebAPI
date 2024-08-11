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
    public class OrderDetailData : IOrderDetailData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OrderDetailData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"SELECT
                    od.OrderId,
                    od.ProductId,
                    od.UnitPrice,
                    od.Qty,
                    od.Discount,
                    p.ProductName,
                    CONCAT(o.ShipName, ', ', o.ShipCity, ', ', o.ShipCountry) AS OrderDetails
                FROM
                    OrderDetails AS od
                INNER JOIN Products p ON od.ProductId = p.ProductId
                INNER JOIN Orders o ON od.OrderId = o.OrderId";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND od." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                sql += "AND (UPPER(p.ProductName) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(CONCAT(o.ShipName, ', ', o.ShipCity, ', ', o.ShipCountry)) LIKE UPPER(CONCAT('%', @filter, '%'))) " +
                       "ORDER BY " + (filters.ColumnOrder ?? "od.OrderId") + " " + (filters.DirectionOrder ?? "asc");
            }

            IEnumerable<OrderDetailDTO> items = await _context.QueryAsync<OrderDetailDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }


        public async Task<OrderDetail> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<OrderDetail>().FindAsync(EmpId);
        }

        public async Task<OrderDetail> Save(OrderDetail entity)
        {
            _context.Set<OrderDetail>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(OrderDetail entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int idOrden, int idProducto)
        {
            int entity = await _context.Set<OrderDetail>().Where(d => d.OrderId == idOrden && d.ProductId == idProducto).ExecuteDeleteAsync();
            return entity;
        }
    }
}
