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
    public class CategoryData : ICategoryData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CategoryData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetDataTable(QueryFilterDto filters)
        {
            var sql = @"
                SELECT
                    category.CategoryId AS Id,
                    category.CategoryName,
                    category.Description
                FROM
                    Categories AS category";

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                sql += @"AND category." + filters.NameForeignKey + " = @foreignKey ";
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                sql += "AND (UPPER(category.CategoryName) LIKE UPPER(CONCAT('%', @filter, '%')) OR UPPER(category.Description) LIKE UPPER(CONCAT('%', @filter, '%'))) ORDER BY " + (filters.ColumnOrder ?? "category.CategoryId") + " " + (filters.DirectionOrder ?? "asc");
            }

            IEnumerable<CategoryDTO> items = await _context.QueryAsync<CategoryDTO>(sql, new { filter = filters.Filter, foreignKey = filters.ForeignKey });

            return items;
        }


        public async Task<Category> GetById(int EmpId)
        {
            return await _context.Set<Category>().FindAsync(EmpId);
        }

        public async Task<Category> Save(Category entity)
        {
            _context.Set<Category>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            int entity = await _context.Set<Category>().Where(d => d.CategoryId== id).ExecuteDeleteAsync();
            return entity;
        }
    }
}
