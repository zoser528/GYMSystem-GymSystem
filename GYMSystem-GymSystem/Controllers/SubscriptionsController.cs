using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GYMSystem.Models;
using GYMSystem_GymSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace GYMSystem.Controllers
{
    [Authorize]

    public class SubscriptionsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Subscriptions
        public IActionResult Index()
        {
            List<Subscription> subscription = _dbContext.Subscriptions.ToList();

            return View(subscription);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Subscriptions.Add(subscription);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Subscription subscription = _dbContext.Subscriptions.Find(id);
            if (subscription == null)
            {
                return NotFound();
            }
            else
            {
                return View(subscription);
            }
        }
        [HttpPost]
        public IActionResult Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(subscription).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_dbContext.Subscriptions.Find(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dbContext.Subscriptions.Remove(_dbContext.Subscriptions.Find(id));
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_dbContext.Subscriptions.Find(id));
        }

    }
}
