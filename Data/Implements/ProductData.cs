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
    public class ProductData : IProductData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetDataTable(QueryFilterDto filters)
        {
            var foreign = false;

            var sql = @"SELECT
                    [Production].[Products].ProductId,
                    [Production].[Products].ProductName,
                    [Production].[Suppliers].CompanyName AS SupplierName,
                    [Production].[Categories].CategoryName AS CategoryName,
                    [Production].[Products].UnitPrice,
                    [Production].[Products].Discontinued
                FROM
                    [Production].[Products]
                INNER JOIN [Production].[Suppliers] ON [Production].[Products].SupplierId = [Production].[Suppliers].SupplierId
                INNER JOIN [Production].[Categories] ON [Production].[Products].CategoryId = [Production].[Categories].CategoryId ";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND [Production].[Products]." + filters.NameForeignKey + " = @foreignKey ";
            }


            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                foreign = true;

                sql += @"WHERE [Production].[Products]." + filters.NameForeignKey + " = @foreignKey ";
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
                    sql += @"[Production].[Products]." + filters.ColumnFilter + " = @filter ";
                }
            }

            sql += "ORDER BY " + (filters.ColumnOrder ?? "[Production].[Products].ProductId") + " " + (filters.DirectionOrder ?? "asc");

            IEnumerable<ProductDTO> items = await _context.QueryAsync<ProductDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }


        public async Task<Product> GetById(int EmpId)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<Product>().FindAsync(EmpId);
        }

        public async Task<Product> Save(Product entity)
        {
            _context.Set<Product>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            int entity = await _context.Set<Product>().Where(d => d.ProductId == id).ExecuteDeleteAsync();
            return entity;
        }
    }
}
