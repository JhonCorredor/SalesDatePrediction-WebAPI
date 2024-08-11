namespace Entity.Dto
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? CustId { get; set; }
        public int EmpId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipperId { get; set; }
        public decimal Freight { get; set; } = 0;
        public string? ShipName { get; set; } = null!;
        public string? ShipAddress { get; set; } = null!;
        public string? ShipCity { get; set; } = null!;
        public string? ShipRegion { get; set; } = null!;
        public string? ShipPostalCode { get; set; } = null!;
        public string? ShipCountry { get; set; } = null!;
        public string? Customer { get; set; } = null!;
        public string? Employee { get; set; } = null!;
        public string? Shipper { get; set; } = null!;
    }
}
