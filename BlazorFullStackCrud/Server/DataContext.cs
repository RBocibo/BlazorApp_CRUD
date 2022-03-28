using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrud.Server
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Parker",
                    position = "Developer",
                    DepartmentId = 1
                },

            new Employee
            {
                Id = 2,
                FirstName = "Bruce",
                LastName = "Wayne",
                position = "Tester",
                DepartmentId = 2
            });

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Software engineering" },
                new Department { DepartmentId = 2, Name = "Testing" });
        }


    }
}
