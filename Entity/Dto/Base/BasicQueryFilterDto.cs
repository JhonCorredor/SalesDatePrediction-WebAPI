namespace Entity.Dto.Base
{
    public class BasicQueryFilterDto
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? Filter { get; set; }
        public string? ColumnFilter { get; set; }
        public string? ColumnOrder { get; set; }
        public string? DirectionOrder { get; set; }
    }
}