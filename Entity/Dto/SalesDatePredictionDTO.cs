namespace Entity.Dto
{
    public class SalesDatePredictionDto
    {
        public int CustId { get; set; }

        public string? CustomerName { get; set; } = null!;
        public string? LastOrderDate { get; set; } = null!;
        public string? NextPredictedOrder { get; set; } = null!;
    }
}
