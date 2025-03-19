using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAcssessLayer.InterFaceses;
using WebApplication1.Models;

namespace WebApplication1.DataAcssessLayer
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Department> GetDepartments()
        {
            return _dbContext.Departments.ToList();
        }
    }
}
