using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Entity.Dto.Base
{
    public class PagedListDto<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public object Lists { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedListDto(List<T> items, int count, int pageNumber = 1, int pageSize = 10, object lists = null)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Lists = lists;

            AddRange((IEnumerable<T>)items);
        }

        public static PagedListDto<T> Create(IEnumerable<T> source, int pageNumber, int pageSize, object lists = null)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListDto<T>(items, count, pageNumber, pageSize, lists);
        }

        public static IEnumerable<T> ApplyDynamicFilters(IEnumerable<T> query, QueryFilterDto queryFilterDto)
        {
            // Agregar aquí lógica para aplicar filtros dinámicos
            // Puedes utilizar reflection para comparar propiedades de cadena con el filtro

            // Ejemplo simple para propiedades de cadena
            query = query.Where(i => EF.Property<string>(i, queryFilterDto.ColumnFilter).Contains(queryFilterDto.Filter));

            return query;
        }

        public static IOrderedQueryable<T> ApplyOrdering(IEnumerable<T> query, QueryFilterDto queryFilterDto)
        {
            // Agregar aquí lógica para ordenar dinámicamente
            // Puedes utilizar reflection para ordenar por la columna especificada

            // Ejemplo simple para ordenar por una propiedad
            if (!string.IsNullOrEmpty(queryFilterDto.ColumnOrder))
            {
                var queryIQueryable = query.AsQueryable();
                query = OrderByProperty(queryIQueryable, queryFilterDto.ColumnOrder, queryFilterDto.DirectionOrder);
            }

            return query as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> OrderByProperty<T>(IQueryable<T> source, string propertyName, string direction)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(
                typeof(Queryable),
                direction == "desc" ? "OrderByDescending" : "OrderBy",
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExp)
            );

            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }
    }
}
