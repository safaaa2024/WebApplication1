using WebApplication1.Migrations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class StudentViewModel
    {
        public string StudentName { get; set; }
        public string Image { get; set; }        
        public string Adress { get; set; } 
        public int Age { get; set; }        
        public List<string> CourseNames { get; set; } = new List<string>();
        public List<string> Color { get; set; } = new List<string>();
        public List<int> Degree { get; set; } = new List<int>();
        public List<int> MinDegree { get; set; } = new List<int>();

      


    }
}
