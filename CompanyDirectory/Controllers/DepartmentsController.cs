using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyDirectory.Data;
using CompanyDirectory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyDirectory.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Остаются только используемые методы
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetTreeData()
        {
            // Загружаем корневые подразделения (без родителя)
            var rootDepartments = await _context.Departments
                .Include(d => d.Children) // Включаем дочерние подразделения
                .Where(d => d.ParentId == null)
                .ToListAsync();

            // Преобразуем в формат для jsTree
            var treeData = rootDepartments.Select(d => new
            {
                id = d.Id,
                text = d.Name,
                children = d.Children.Select(c => new
                {
                    id = c.Id,
                    text = c.Name
                }).ToList()
            }).ToList();

            return Json(treeData);
        }

        // Controllers/DepartmentsController.cs
        public IActionResult Create()
        {
            ViewBag.ParentDepartments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FormationDate,Description,ParentId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "DepartmentsView");
            }
            ViewBag.ParentDepartments = new SelectList(_context.Departments, "Id", "Name", department.ParentId);
            return View(department);
        }

        [HttpPost]
        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Загружаем список подразделений для выпадающего списка (кроме текущего и его дочерних)
            var departments = await _context.Departments
                .Where(d => d.Id != id && d.ParentId != id) // исключаем текущее и его дочерние
                .ToListAsync();

            ViewBag.ParentDepartments = new SelectList(departments, "Id", "Name", department.ParentId);
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FormationDate,Description,ParentId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "DepartmentsView");
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return Json(new { success = false });

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}