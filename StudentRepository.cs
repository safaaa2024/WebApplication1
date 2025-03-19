using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAcssessLayer.InterFaceses;
using WebApplication1.Migrations;
using WebApplication1.Models;

namespace WebApplication1.DataAcssessLayer
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
          _appDbContext = appDbContext;
        }

        public int addStudent(StudentModel student)
        {
            _appDbContext.Students.Add(student);
          return  _appDbContext.SaveChanges();
        }

        public int DeleteStudent(StudentModel studentModel)
        {
            _appDbContext.Remove(studentModel);
           return _appDbContext.SaveChanges();
        }

        public StudentModel? Find(int id)
        {
           return _appDbContext.Students.Find(id);
        }

        public StudentModel? GetStudentById(int id)
        {
           return _appDbContext.Students
                .Include(s => s.CourseStudents)
                .ThenInclude(cs => cs.Course)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<StudentModel> GetStudents(Studen studen)
        {
           return _appDbContext.Students.ToList();
        }

        public List<StudentModel> GetStudents()
        {
          return _appDbContext.Students.ToList();
        }

        public int UpdateStudent(StudentModel student)
        {
            _appDbContext.Students.Update(student);
           return _appDbContext.SaveChanges();
        }
    }
}
