using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("OrderDetails", Schema = "Sales")]
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "smallint")]
        [Range(1, short.MaxValue)]
        public short Qty { get; set; } = 1;

        [Column(TypeName = "numeric(4, 3)")]
        [Range(0, 1)]
        public decimal Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}