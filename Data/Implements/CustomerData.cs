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
    public class CustomerData : ICustomerData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CustomerData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"SELECT
                    customer.CustId AS Id,
                    customer.CompanyName,
                    customer.ContactName,
                    customer.ContactTitle,
                    customer.Address,
                    customer.City,
                    customer.Region,
                    customer.PostalCode,
                    customer.Country,
                    customer.Phone,
                    customer.Fax
                FROM
                    Sales.Customers AS customer";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND customer." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                sql += "AND (UPPER(customer.CompanyName) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(customer.ContactName) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(customer.ContactTitle) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(customer.City) LIKE UPPER(CONCAT('%', @filter, '%')) OR " +
                       "UPPER(customer.Country) LIKE UPPER(CONCAT('%', @filter, '%'))) " +
                       "ORDER BY " + (filters.ColumnOrder ?? "customer.CustId") + " " + (filters.DirectionOrder ?? "asc");
            }

            IEnumerable<CustomerDTO> items = await _context.QueryAsync<CustomerDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }

        public async Task<IEnumerable<SalesDatePredictionDto>> GetSalesDatePrediction(QueryFilterDto filters)
        {
            var foreign = false;

            var sql = @"WITH OrderIntervals AS (
                SELECT 
                   custid, 
                   DATEDIFF(day, LAG(orderdate) OVER (PARTITION BY custid ORDER BY orderdate), orderdate) AS IntervalDays
                FROM 
                   [Sales].[Orders]
                ),
                AvgOrderIntervals AS (
                    SELECT 
                       custid, 
                       AVG(IntervalDays) AS AvgIntervalDays
                    FROM 
                       OrderIntervals
                    WHERE 
                      IntervalDays IS NOT NULL
                    GROUP BY custid
                    ),
                    LastOrderDates AS (
                       SELECT 
                           custid, 
                           MAX(orderdate) AS LastOrderDate
                       FROM 
                           [Sales].[Orders]
                       GROUP BY 
                           custid
                    )
                    SELECT 
                        C.CustId,
                        C.companyname AS CustomerName,
                        L.LastOrderDate AS LastOrderDate,
                        DATEADD(day, A.AvgIntervalDays, L.LastOrderDate) AS NextPredictedOrder
                    FROM 
                        [Sales].[Customers] C
                        INNER JOIN LastOrderDates L ON C.custid = L.custid
                        INNER JOIN AvgOrderIntervals A ON C.custid = A.custid ";

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
                    sql += "C." + filters.ColumnFilter + " LIKE @filter ";
                }
            }

            sql += "ORDER BY " + (filters.ColumnOrder ?? "C.CustId") + " " + (filters.DirectionOrder ?? "asc");

            var filterValue = "%" + filters.Filter + "%";
            IEnumerable<SalesDatePredictionDto> items = await _context.QueryAsync<SalesDatePredictionDto>(sql, new { filter = filterValue, foreignKey = filters.ForeignKey });

            return items;
        }


        public async Task<Customer> GetById(int CustId)
        {
            var sql = @"SELECT * FROM Sales.Customers WHERE CustId = @custId";
            IEnumerable<Customer> items = await _context.QueryAsync<Customer>(sql, new { custId = CustId });
            return items.First();
        }

        public async Task<Customer> Save(Customer entity)
        {
            _context.Set<Customer>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            int entity = await _context.Set<Customer>().Where(d => d.CustId == id).ExecuteDeleteAsync();
            return entity;
        }
    }
}
