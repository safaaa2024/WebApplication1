namespace WebApplication1.ViewModels
{
    public class CoursViewModel
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Instructor { get; set; }
        public int MinDegree { get; set; }
        public int Id { get; set; }
        public List<int> Degree { get; set; } = new List<int>();
        public List<string> StudentName { get; set; } = new List<string>();
        public List<string> Color { get; set; } = new List<string>();
    }
}
