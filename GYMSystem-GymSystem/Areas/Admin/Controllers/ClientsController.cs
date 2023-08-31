using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GYMSystem_GymSystem.Models;
using Microsoft.AspNetCore.Authorization;
using GYMSystem_GymSystem.Data;
using GYMSystem.Models;
using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;

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
        public DateAndTime TimeNow { get; set; }
        
        
        // GET: Admin/Clients
        public IActionResult Index()
        {
            var client = GetClients();

            return View(client);
        }
        private List<Client> GetClients()
        {
            var clients = (from Client in _context.Clients
                           join department in _context.Departments on Client.Departmentid equals department.DepartmentId
                           join trainer in _context.Trainers on Client.Trainerid equals trainer.TrainerId
                           join subscription in _context.Subscriptions on Client.Subscriptionid equals subscription.SubscriptionId
                           join branch in _context.Branches on Client.Branchid equals branch.BranchId

                           select new Client
                           {
                               ClientId = Client.ClientId,
                               ClientName = Client.ClientName,
                               ClientNumber = Client.ClientNumber,
                               DOB = Client.DOB,
                               ClientAddress = Client.ClientAddress,
                               Departmentid = Client.Departmentid,
                               DepartmentName = department.DepartmentName,
                               Trainerid = Client.Trainerid,
                               TrainerName = trainer.TrainerName,
                               Subscriptionid = Client.Subscriptionid,
                               SubscriptionName = subscription.SubscriptionName,
                               Branchid = Client.Branchid,
                               BranchName = branch.BranchName,
                               End_Subscription = Client.End_Subscription,
                               pay = Client.pay,
                               Active = Client.Active

                           }).ToList();
            return clients;
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
            if (ModelState.IsValid)
            {
                ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation");
                ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode");
                ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName");
                ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName");
                return View();
            }
            else
            {
                return NotFound(); 
            }

        }

        // POST: Admin/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,UserName,UserPassword,ClientNumber,DOB,SubscriptionDate,ClientAddress,ClientEmail,Branchid,Departmentid,Trainerid,Subscriptionid")] Client client)
        {
            
            ModelState.Remove("Branch");
            ModelState.Remove("BranchName");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("SubscriptionName");
            ModelState.Remove("Subscription");
            ModelState.Remove("TrainerName");
            ModelState.Remove("TrainerId");
            var subscription = await _context.Subscriptions.FindAsync(client.Subscriptionid);
            
            if (ModelState.IsValid)
            {
                client.End_Subscription = (DateTime.Now.AddMonths(subscription.SubscriptionPeriod));
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
