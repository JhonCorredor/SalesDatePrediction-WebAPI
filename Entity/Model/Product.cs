using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Entity.Model
{
    [Table("Products", Schema = "Production")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(40)]
        public string ProductName { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; } = 0;

        public bool Discontinued { get; set; } = false;

        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}