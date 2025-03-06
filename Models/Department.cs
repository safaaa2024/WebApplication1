namespace WebApplication1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<StudentModel> Students { get; set; } = new List<StudentModel>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
