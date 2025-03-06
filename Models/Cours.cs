using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class Cours
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [MinLength(2, ErrorMessage ="Name Less Than 3")]
        [MaxLength(25, ErrorMessage = "Name Bigger Than 25")]
        [Remote("NameValidation", "Courses", ErrorMessage = "This Name Is Exists")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Topic is Required")]
        [MinLength(3, ErrorMessage = "Topic Less Than 3")]
        [MaxLength(25, ErrorMessage = "Topic Bigger Than 25")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "MinimumDegree is Required")]
        [Range(0, 100, ErrorMessage = "MinimumDegree must be a positive number")]
        public int MinimumDegree { get; set; }
        [Required(ErrorMessage = "FullDegree is Required")]
        [Range(0, 100, ErrorMessage = "FullDegree must be a positive number")]
        [Remote("FullMinDegreeValidation", "Courses", ErrorMessage ="Full Degree Must be Greater than MinDegree",AdditionalFields = "MinimumDegree")]
        public int FullDegree { get; set; }
        public Instructor? instructor { get; set; }
        public int? instructorId { get; set; }
        public List<Cours_stds> CourseStudents { get; set; } = new List<Cours_stds>();
    }
}
