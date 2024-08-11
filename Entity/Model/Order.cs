using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Orders", Schema = "Sales")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? CustId { get; set; }

        public int EmpId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int ShipperId { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        public decimal Freight { get; set; }

        [Required, MaxLength(40)]
        public string ShipName { get; set; }

        [Required, MaxLength(60)]
        public string ShipAddress { get; set; }

        [Required, MaxLength(15)]
        public string ShipCity { get; set; }

        [MaxLength(15)]
        public string ShipRegion { get; set; }

        [MaxLength(10)]
        public string ShipPostalCode { get; set; }

        [Required, MaxLength(15)]
        public string ShipCountry { get; set; }

        [ForeignKey(nameof(CustId))]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(EmpId))]
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(ShipperId))]
        public virtual Shipper Shipper { get; set; }
    }
}