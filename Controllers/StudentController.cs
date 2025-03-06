using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Migrations;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext AppDb = new AppDbContext();
        public IActionResult Index()
        {
            var students = AppDb.Students.ToList();
            if (TempData.ContainsKey("Name"))
            {
                TempData["Name"].ToString();
                TempData.Keep("Name");
            }
            return View(students);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var students = AppDb.Students.Find(id);
            if (students == null)
            {
                return NotFound();
            }
            ViewBag.Departments = AppDb.Departments.ToList();
            return View(students);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                AppDb.Students.Update(student);
                AppDb.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = AppDb.Departments.ToList();
            return View(student);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = AppDb.Students.Find(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(StudentModel studentModel)
        {

            AppDb.Remove(studentModel);
            int count = AppDb.SaveChanges();
            if (count > 0)
                return RedirectToAction(nameof(Index));            
            return View(studentModel);
            
        }
        public IActionResult Create()
        {
            ViewBag.Departments = AppDb.Departments.ToList();
            return View(new StudentModel());
        }
        [HttpPost]
        public IActionResult Create(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                AppDb.Students.Add(studentModel);
                AppDb.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = AppDb.Departments.ToList();
            return View(studentModel);
        }
        public IActionResult Details(int? id)
        {          
            var student = AppDb.Students
                .Include(s => s.CourseStudents)
                .ThenInclude(cs => cs.Course)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            StudentViewModel viewModel = new StudentViewModel();
            viewModel.StudentName = student.Name;
            viewModel.Image = student.Image;
            viewModel.Adress = student.Address;
            viewModel.Age = student.Age;
            var StCourses = student.CourseStudents
                .Select(Sc => new StudentCourseViewModel {Degree= Sc.Degree,CouseName= Sc.Course.Name,MinDegree= Sc.Course.MinimumDegree }).ToList();
            foreach (var Course in StCourses)
            {
                viewModel.Degree.Add(Course.Degree);
                viewModel.MinDegree.Add(Course.MinDegree);
                viewModel.CourseNames.Add(Course.CouseName);
                if (Course.Degree <= Course.MinDegree)
                {
                    viewModel.Color.Add("Red");
                }
                else
                    viewModel.Color.Add("Green");
            }
            TempData["Name"] = $"Student:{viewModel.StudentName}";
            return View(viewModel);
           
        }
         
    }
}
