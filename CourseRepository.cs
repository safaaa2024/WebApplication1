using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAcssessLayer.InterFaceses;
using WebApplication1.Models;

namespace WebApplication1.DataAcssessLayer
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _dbContext;

        public CourseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddCourse(Cours course)
        {
            _dbContext.Add(course);
            return _dbContext.SaveChanges();
        }

        public List<Cours> GetAllCourses()
        {
            return _dbContext.Courses.Include(c => c.instructor).ToList();
        }

        public Cours? GetCourseById(int id)
        {
            return _dbContext.Courses.Find(id);
        }

        public bool GetCourseByName(string Name)
        {
            return _dbContext.Courses.Any(c => c.Name == Name);
        }

        public Cours? GetCourseWithInclude(int id)
        {
          return _dbContext.Courses.Include(c => c.instructor).
                                             Include(CS => CS.CourseStudents).
                                            ThenInclude(s => s.Student).
                                            FirstOrDefault(S => S.Id == id);
        }

        public int UpdateCourse(Cours course)
        {
            _dbContext.Courses.Update(course);
            return _dbContext.SaveChanges();

        }
    }
}
