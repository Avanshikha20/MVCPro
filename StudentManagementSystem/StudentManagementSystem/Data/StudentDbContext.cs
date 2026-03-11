using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);

            // Seed Departments
            modelBuilder.Entity<Department>().HasData(

                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "IT",
                    Location = "Block A"
                },

                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "HR",
                    Location = "Block B"
                },

                new Department
                {
                    DepartmentId = 3,
                    DepartmentName = "Finance",
                    Location = "Block C"
                }
            );

            
        }

    }
}