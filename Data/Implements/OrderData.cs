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
    public class OrderData : IOrderData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OrderData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetDataTable(QueryFilterDto filters)
        {
            var foreign = false;

            var sql = @"SELECT
                    Sales.Orders.OrderId,
                    Sales.Orders.CustId,
                    Sales.Customers.CompanyName AS CustomerName,
                    Sales.Orders.EmpId,
                    HR.Employees.FirstName + ' ' + HR.Employees.LastName AS EmployeeName,
                    Sales.Orders.OrderDate,
                    Sales.Orders.RequiredDate,
                    Sales.Orders.ShippedDate,
                    Sales.Orders.ShipperId,
                    Sales.Shippers.CompanyName AS ShipperName,
                    Sales.Orders.Freight,
                    Sales.Orders.ShipName,
                    Sales.Orders.ShipAddress,
                    Sales.Orders.ShipCity,
                    Sales.Orders.ShipRegion,
                    Sales.Orders.ShipPostalCode,
                    Sales.Orders.ShipCountry
                FROM
                    Sales.Orders
                INNER JOIN Sales.Customers ON Sales.Orders.CustId = Sales.Customers.CustId
                INNER JOIN HR.Employees ON Sales.Orders.EmpId = HR.Employees.EmpId
                INNER JOIN Sales.Shippers  ON Sales.Orders.ShipperId =Sales.Shippers.ShipperId ";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                foreign = true;

                sql += @"WHERE Sales.Orders." + filters.NameForeignKey + " = @foreignKey ";
            }


            if (!string.IsNullOrEmpty(filters.Filter))
            {
                if (!foreign)
                {
                    sql += "WHERE ";
                }
                else
                {
                    sql += "AND ";
                }

                if (!string.IsNullOrEmpty(filters.ColumnFilter))
                {
                    sql += @"Sales.Orders." + filters.ColumnFilter + " = @filter ";
                }
            }

            sql += "ORDER BY " + (filters.ColumnOrder ?? "Sales.Orders.OrderId") + " " + (filters.DirectionOrder ?? "asc");


            IEnumerable<OrderDTO> items = await _context.QueryAsync<OrderDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }

        public async Task<Order> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<Order>().FindAsync(EmpId);
        }

        public async Task<Order> Save(Order entity)
        {
            _context.Set<Order>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int EmpId)
        {
            int entity = await _context.Set<Order>().Where(d => d.EmpId == EmpId).ExecuteDeleteAsync();
            return entity;
        }
    }
}
