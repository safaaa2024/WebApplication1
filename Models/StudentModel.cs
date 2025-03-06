namespace WebApplication1.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public Department? department { get; set; }
        public int? departmentId { get; set; }
        public List<Cours_stds> CourseStudents { get; set; } = new List<Cours_stds>();

    }
}
