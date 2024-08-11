namespace Entity.Dto
{
    public class EmployeeDTO
    {

        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
