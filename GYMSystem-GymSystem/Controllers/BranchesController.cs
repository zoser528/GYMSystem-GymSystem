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

    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BranchesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Branch> branch = _dbContext.Branches.ToList();

            return View(branch);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Branches.Add(branch);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            else
            {
                return View(branch);
            }
        }
        [HttpPost]
        public IActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(branch).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            else
            {
                return View(branch);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Branch branch = _dbContext.Branches.Find(id);
            _dbContext.Branches.Remove(branch);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            else
            {
                return View(branch);
            }
        }
    }
}
