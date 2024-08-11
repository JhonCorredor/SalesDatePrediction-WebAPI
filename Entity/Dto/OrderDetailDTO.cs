namespace Entity.Dto
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; } = 0;
        public short Qty { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public string? Order { get; set; } = null!;
        public string? Product { get; set; } = null!;
    }

}
