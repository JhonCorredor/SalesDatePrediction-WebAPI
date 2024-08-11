using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Categories", Schema = "Production")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(15)]
        public string CategoryName { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }
    }
}