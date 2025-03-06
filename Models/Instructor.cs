namespace WebApplication1.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public decimal Salary { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public Department? department { get; set; }
        public int? departmentId { get; set; }
        public DateTime HireDate { get; set; }
        public List<Cours> courses { get; set; } = new List<Cours>();
    }
}
