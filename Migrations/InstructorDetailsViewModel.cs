namespace WebApplication1.Migrations
{
    public class InstructorDetailsViewModel
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public DateTime HiringDate { get; set; }
        public List<string> CourseNames { get; set; } = new List<string>();
    }
}
