using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GYMSystem_GymSystem.Data;
using GYMSystem_GymSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace GYMSystem_GymSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminRole")]

    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Clients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clients.Include(c => c.Branch).Include(c => c.Department).Include(c => c.Subscription).Include(c => c.Trainer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Branch)
                .Include(c => c.Department)
                .Include(c => c.Subscription)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Admin/Clients/Create
        public IActionResult Create()
        {
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation");
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode");
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName");
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName");
            return View();
        }

        // POST: Admin/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,UserName,UserPassword,ClientNumber,DOB,SubscriptionDate,ClientAddress,ClientEmail,Branchid,Departmentid,Trainerid,Subscriptionid")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation", client.Branchid);
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode", client.Departmentid);
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName", client.Subscriptionid);
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName", client.Trainerid);
            return View(client);
        }

        // GET: Admin/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation", client.Branchid);
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode", client.Departmentid);
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName", client.Subscriptionid);
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName", client.Trainerid);
            return View(client);
        }

        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,UserName,UserPassword,ClientNumber,DOB,SubscriptionDate,ClientAddress,ClientEmail,Branchid,Departmentid,Trainerid,Subscriptionid")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation", client.Branchid);
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode", client.Departmentid);
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName", client.Subscriptionid);
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName", client.Trainerid);
            return View(client);
        }

        // GET: Admin/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Branch)
                .Include(c => c.Department)
                .Include(c => c.Subscription)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Admin/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
