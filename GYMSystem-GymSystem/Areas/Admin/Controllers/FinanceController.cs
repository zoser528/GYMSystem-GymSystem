using GYMSystem.Models;
using GYMSystem_GymSystem.Data;
using GYMSystem_GymSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace GYMSystem_GymSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminRole")]
    public class FinanceController : Controller
    {

        private readonly ApplicationDbContext _context;

        public FinanceController(ApplicationDbContext context)
        {
            _context = context;
        }
        private List<Client> GetClients()
        {
            var clients = (from Client in _context.Clients
                           join department in _context.Departments on Client.Departmentid equals department.DepartmentId
                           join trainer in _context.Trainers on Client.Trainerid equals trainer.TrainerId
                           join subscription in _context.Subscriptions on Client.Subscriptionid equals subscription.SubscriptionId
                           join branch in _context.Branches on Client.Branchid equals branch.BranchId
                           where !Client.pay


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
        public async Task<IActionResult> Index()
        {
            
            var inactiveClients = GetClients();
                

            return View(inactiveClients);
        }
        public async Task<IActionResult> Active(int? id)
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

        public async Task<IActionResult> Done(int? id)
        {
            var client = await _context.Clients
                .Include(c => c.Branch)
                .Include(c => c.Department)
                .Include(c => c.Subscription)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.ClientId == id);

            client.pay = true;

            DateTime currentDateTime = DateTime.Now;

            if (client.End_Subscription > currentDateTime)
            {
                client.Active = true;
            }
            else
            {
                client.Active = false;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //Subscription ses1 = _context.Subscriptions.Find(id);
        public async Task<IActionResult> Invoice (int? id)
        {
            var client = await _context.Clients
                .Include(c => c.Branch)
                .Include(c => c.Department)
                .Include(c => c.Subscription)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.ClientId == id);

            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            if (client == null)
            {
                return NotFound();
            }
            var test = client.Subscription.SubscriptionId;
            // Sub for invoice
            ViewData["SubscriptionName"] = client.Subscription.SubscriptionName;
            ViewData["SubscriptionPrice"] = client.Subscription.SubscriptionPrice;
            ViewData["Period"] = client.Subscription.SubscriptionPeriod;
            ViewBag.SubscriptionName = client.Subscription.SubscriptionName;

            // Trainer for invoice
            ViewData["TrainerName"] = client.Trainer.TrainerName;
            ViewData["SubscriptionSalary"] = client.Trainer.SubscriptionSalary;


            // Branch for invoice
            ViewData["BranchName"] = client.Branch.BranchName;

            return View(client);
        }
        
        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Save(int id, [Bind("ClientId,ClientName,UserName,UserPassword,ClientNumber,DOB,SubscriptionDate,ClientAddress,ClientEmail,Branchid,Departmentid,Trainerid,Subscriptionid")] Client client)
        {
            var subscription = await _context.Subscriptions.FindAsync(client.Subscriptionid);

            var Period = subscription.SubscriptionPeriod;


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

                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SubscriptionPeriod = Period;
            ViewData["Period"] = Period;
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation", client.Branchid);
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode", client.Departmentid);
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName", client.Subscriptionid);
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName", client.Trainerid);
            return View(client);
        }
        

    }
}
