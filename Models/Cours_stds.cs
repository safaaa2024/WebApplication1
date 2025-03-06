using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Cours_stds
    {
        public int CourseId { get; set; } // Foreign key لـ Course
        public int StudentId { get; set; } // Foreign key لـ Student
        public int Degree { get; set; }

        [ForeignKey("CourseId")]
        public Cours Course { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel Student { get; set; }
    }
}
