using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAcssessLayer.InterFaceses;
using WebApplication1.Models;

namespace WebApplication1.DataAcssessLayer
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _dbContext;

        public InstructorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddInstructor(Instructor instructor)
        {
            _dbContext.Instructors.Add(instructor);
            return _dbContext.SaveChanges();
        }

        public int DeleteInstructor(Instructor Instructor)
        {
            _dbContext.Remove(Instructor);
            return _dbContext.SaveChanges();
        }
      
        public List<Instructor> GetInstructors()
        {
            return _dbContext.Instructors.ToList();
        }

        public List<Instructor> GetInstructorByDeptId(int id)
        {
            return _dbContext.Instructors.Where(i => i.departmentId == id).ToList();
        }

        public Instructor? Find(int id)
        {
            return _dbContext.Instructors.Find(id);
        }

        public int UpdateInstructor(Instructor instructor)
        {
            _dbContext.Instructors.Update(instructor);
            return _dbContext.SaveChanges();
        }

        public Instructor? GetInstructorById(int id)
        {
            return _dbContext.Instructors
               .Include(i => i.courses)
               .FirstOrDefault(i => i.Id == id);
        }
    }
}
