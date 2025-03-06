using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Migrations;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller
    {
        AppDbContext AppDbContext = new AppDbContext();
        public IActionResult Index()
        {
            var instructors = AppDbContext.Instructors.ToList();
            return View(instructors);
        }
        
  
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = AppDbContext.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }
            ViewBag.Departments = AppDbContext.Departments.ToList();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                AppDbContext.Instructors.Update(instructor);
                AppDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = AppDbContext.Departments.ToList();
            return View(instructor);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = AppDbContext.Departments.ToList();
            return View(new Instructor());

        }
        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                AppDbContext.Instructors.Add(instructor);
                AppDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = AppDbContext.Departments.ToList();
            return View(instructor);

        }

        public IActionResult Details(int id)
        {
            var instructor = AppDbContext.Instructors
                .Include(i => i.courses)               
                .FirstOrDefault(i => i.Id == id);

            if (instructor == null)
            {
                return NotFound();
            }

            var viewModel = new InstructorDetailsViewModel
            {
                FullName = $"{instructor.FirstName} {instructor.LastName}",
                Image = instructor.Image,
                HiringDate = instructor.HireDate,
                CourseNames = instructor.courses
                    .Select(ic => ic.Name)
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
