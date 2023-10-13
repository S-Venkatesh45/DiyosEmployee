using DiyosEmployee.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DiyosEmployee.Data
{
    public class DiyosEmployeeDbContext : DbContext
    {
        public DiyosEmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
