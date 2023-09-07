using GYMSystem_GymSystem.Data;
using GYMSystem_GymSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYMSystem_GymSystem.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
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
                if (trainer.Counter != null)
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
                return RedirectToAction("Index","Home",new { area = "default" });
            }
            ViewData["Branchid"] = new SelectList(_context.Branches, "BranchId", "BranchLocation", client.Branchid);
            ViewData["Departmentid"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentCode", client.Departmentid);
            ViewData["Subscriptionid"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionName", client.Subscriptionid);
            ViewData["Trainerid"] = new SelectList(_context.Trainers, "TrainerId", "TrainerName", client.Trainerid);
            return View(client);
        }

    }
}
