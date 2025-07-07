using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyDirectory.Data;
using CompanyDirectory.Models;
using CompanyDirectory.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyDirectory.Controllers
{
    public class DepartmentsViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Загружаем все подразделения с их сотрудниками и родительскими отделами
            var departments = await _context.Departments
                .Include(d => d.Employees)  // Загружаем сотрудников
                .Include(d => d.Parent)     // Загружаем родительское подразделение
                .Select(d => new DepartmentInfo
                {
                    Id = d.Id,
                    Name = d.Name ?? "Без названия", // Если название пустое - заменяем текстом
                    FormationDate = d.FormationDate,
                    Description = d.Description ?? "", // Если описание пустое - заменяем пустой строкой
                    EmployeeCount = d.Employees.Count, // Считаем количество сотрудников
                    ParentId = d.ParentId,
                    ParentName = d.Parent != null ? d.Parent.Name : "Нет" // Название родительского отдела
                })
                .ToListAsync();

            return View(departments);
        }
    }
}