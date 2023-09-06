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
using System.Xml.Linq;
using OfficeOpenXml;

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
            var trainer = await _context.Trainers.FindAsync(client.Trainerid);

            
            if (ModelState.IsValid)
            {
                if(trainer.Counter != null)
                {
                    trainer.Counter++;
                }
                else
                {
                    trainer.Counter = 0;
                }
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
            var trainer = await _context.Trainers.FindAsync(client.Trainerid);
            if (client == null)
            {
                return NotFound();
            }
            if (trainer != null)
            {
                trainer.Counter = trainer.Counter - 1;
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
            var trainer = await _context.Trainers.FindAsync(client.Trainerid);

            if (id != client.ClientId)
            {
                return NotFound();
            }

            ModelState.Remove("Branch");
            ModelState.Remove("BranchName");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("SubscriptionName");
            ModelState.Remove("Subscription");
            ModelState.Remove("TrainerName");
            ModelState.Remove("TrainerId");
            if (ModelState.IsValid)
            {
                try
                {
                    trainer.Counter += 1;
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
            var trainer = await _context.Trainers.FindAsync(client.Trainerid);

            if (client != null)
            {
                if(trainer.Counter != null)
                {
                    trainer.Counter = trainer.Counter - 1;
                }   
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
        

        public IActionResult ExportToExcel()
        {
            var data = GetClients();


            // Set the EPPlus LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // For non-commercial use

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Set the header row
                worksheet.Cells[1, 1].Value = "Client Name";
                worksheet.Cells[1, 2].Value = "Client Number";
                worksheet.Cells[1, 3].Value = "date of birth";
                worksheet.Cells[1, 4].Value = "Time to End Subscription";
                worksheet.Cells[1, 5].Value = "Client Address";
                worksheet.Cells[1, 6].Value = "Department Name";
                worksheet.Cells[1, 7].Value = "Branch Name";
                worksheet.Cells[1, 8].Value = "Subscription Name";
                worksheet.Cells[1, 9].Value = "Trainer Name";
                worksheet.Cells[1, 10].Value = "is Active!";
                worksheet.Cells[1, 11].Value = "paid";
                // Add more headers for each column as needed

                // Fill data rows
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = data[i].ClientName;
                    worksheet.Cells[i + 2, 2].Value = data[i].ClientNumber;
                    worksheet.Cells[i + 2, 3].Value = data[i].DOB;
                    worksheet.Cells[i + 2, 4].Value = data[i].End_Subscription;
                    worksheet.Cells[i + 2, 5].Value = data[i].ClientAddress;
                    worksheet.Cells[i + 2, 6].Value = data[i].DepartmentName;
                    worksheet.Cells[i + 2, 7].Value = data[i].BranchName;
                    worksheet.Cells[i + 2, 8].Value = data[i].SubscriptionName;
                    worksheet.Cells[i + 2, 9].Value = data[i].TrainerName;
                    worksheet.Cells[i + 2, 10].Value = data[i].Active;
                    worksheet.Cells[i + 2, 11].Value = data[i].pay;
                    // Add more data columns as needed
                }

                // Set content type and file name
                Response.Headers.Add("Content-Disposition", "attachment; filename=ExportedData.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Write the Excel package to the response stream
                Response.Body.Write(package.GetAsByteArray());
            }

            return new EmptyResult();
        }
    }
}
