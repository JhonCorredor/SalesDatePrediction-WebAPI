namespace Entity.Dto.Base
{
    public class QueryFilterDto : BasicQueryFilterDto
    {
        public decimal? Extra { get; set; }
        public int? ForeignKey { get; set; }
        public string? NameForeignKey { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? Periodicity { get; set; }
    }
}
