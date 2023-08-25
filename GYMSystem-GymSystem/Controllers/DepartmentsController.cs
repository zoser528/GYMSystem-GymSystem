using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GYMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using GYMSystem_GymSystem.Models;
using GYMSystem_GymSystem.Data;

namespace GYMSystem.Controllers
{
    [Authorize("AdminRole")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Departments
        public IActionResult Index()
        {
            List<Department> department = _dbContext.Departments.ToList();

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Add(department);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            else {
                return View(department);
            }
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(department).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View(department);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            _dbContext.Departments.Remove(department);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View(department);
            }
        }




    }
}
