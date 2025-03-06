using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        
        AppDbContext AppDbContext = new AppDbContext();
        public IActionResult FullMinDegreeValidation(int FullDegree,int MinimumDegree)
        {
            if (MinimumDegree >= FullDegree)
                return Json(false);
            return Json(true);
        }        
        public IActionResult NameValidation(string Name)
        {
            var Check = AppDbContext.Courses.Any(c => c.Name == Name);
            if (Check )
                return Json(false);
            return Json(true);
        }
        public IActionResult Index()
        {
            var courses = AppDbContext.Courses.Include(c => c.instructor).ToList();
            if (TempData.ContainsKey("Name"))
            {
                TempData["Name"].ToString();
                TempData.Keep("Name");
            }
            return View(courses);
        }
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return BadRequest();

            var course = AppDbContext.Courses.Find(id);
            if (course is not null)
            {
                ViewBag.Instructors = AppDbContext.Instructors.ToList();
                return View(course);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Cours course)
        {
            if (id != course.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                AppDbContext.Courses.Update(course);
                AppDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instructors = AppDbContext.Instructors.ToList();

            return View(course);
        }

        public IActionResult Create()
        {
            ViewBag.instructor = AppDbContext.Instructors.ToList();
            return View(new Cours());

        }
        [HttpPost]
        public IActionResult Create(Cours Cours)
        {
            if (ModelState.IsValid)
            {
                AppDbContext.Courses.Add(Cours);
                AppDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instructors = AppDbContext.Instructors.ToList();
            return View(Cours);

        }

        public IActionResult Details(int? id)
        {
            var course = AppDbContext.Courses.Include(c => c.instructor).
                                             Include(CS=>CS.CourseStudents).
                                            ThenInclude(s=>s.Student).
                                            FirstOrDefault(S => S.Id == id);
            if (course is not null)
            {
                var students = course.CourseStudents.Select(cs => new { StudentName = cs.Student.Name, cs.Degree });
                var coursVm = new CoursViewModel()
                {
                    Id = course.Id,
                    Topic = course.Topic,
                    Name = course.Name,
                    Instructor =$"{course.instructor.FirstName} {course.instructor.LastName}",
                   MinDegree=course.MinimumDegree
                };
                foreach (var item in students)
                {
                    coursVm.StudentName.Add(item.StudentName);
                    coursVm.Degree.Add(item.Degree);
                    if (item.Degree<coursVm.MinDegree)
                    {
                        coursVm.Color.Add("Red");
                    }
                    else
                    {
                        coursVm.Color.Add("Green");
                    }
                }
                return View(coursVm);
            }
            return Redirect(nameof(Index));
        }
    }
}
