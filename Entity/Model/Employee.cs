using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Employees", Schema = "HR")]
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, MaxLength(10)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string Title { get; set; }

        [Required, MaxLength(25)]
        public string TitleOfCourtesy { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

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

        public int? MgrId { get; set; }

        [ForeignKey(nameof(MgrId))]
        public virtual Employee Manager { get; set; }
    }
}
