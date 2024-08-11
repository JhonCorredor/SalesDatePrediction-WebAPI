using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Suppliers", Schema = "Production")]
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required, MaxLength(40)]
        public string CompanyName { get; set; }

        [Required, MaxLength(30)]
        public string ContactName { get; set; }

        [Required, MaxLength(30)]
        public string ContactTitle { get; set; }

        [Required, MaxLength(60)]
        public string Address { get; set; }

        [Required, MaxLength(15)]
        public string City { get; set; }

        [MaxLength(15)]
        public string Region { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Required, MaxLength(15)]
        public string Country { get; set; }

        [Required, MaxLength(24)]
        public string Phone { get; set; }

        [MaxLength(24)]
        public string Fax { get; set; }
    }
}