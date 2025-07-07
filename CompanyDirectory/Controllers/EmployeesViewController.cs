using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyDirectory.Data;
using CompanyDirectory.Models;
using CompanyDirectory.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyDirectory.Controllers
{
    public class EmployeesViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeInfo
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    Position = e.Position,
                    HasDrivingLicense = e.HasDrivingLicense,
                    DepartmentName = e.Department!.Name // ! - уверены, что Department не null
                })
                .ToListAsync();

            return View(employees);
        }
    }
}