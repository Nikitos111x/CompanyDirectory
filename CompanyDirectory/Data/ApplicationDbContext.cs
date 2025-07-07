using Microsoft.EntityFrameworkCore;
using CompanyDirectory.Models;

namespace CompanyDirectory.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи Подразделение-Сотрудники
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)   // У подразделения много сотрудников
                .WithOne(e => e.Department)  // У сотрудника одно подразделение
                .HasForeignKey(e => e.DepartmentId) // Внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            // Настройка иерархии подразделений
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Children)   // У подразделения много дочерних
                .WithOne(d => d.Parent)     // У дочернего одно родительское
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Запрещаем каскадное удаление
        }
    }
}