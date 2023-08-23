using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GYMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using GYMSystem_GymSystem.Data;


namespace GYMSystem.Controllers
{
    [Authorize]

    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var client = GetClients();

            return View(client);
        }
        private List<Client> GetClients()
        {
            var clients = (from Client in _dbContext.Clients
                             join department in _dbContext.Departments on Client.Departmentid equals department.DepartmentId
                             join trainer in _dbContext.Trainers on Client.Trainerid equals trainer.TrainerId
                             join subscription in _dbContext.Subscriptions on Client.Subscriptionid equals subscription.SubscriptionId
                             join branch in _dbContext.Branches on Client.Branchid equals branch.BranchId
                             
                             select new Client
                             {
                                 ClientId = Client.ClientId,
                                 ClientName = Client.ClientName,
                                 ClientNumber = Client.ClientNumber,
                                 DOB = Client.DOB,
                                 SubscriptionDate = Client.SubscriptionDate,
                                 ClientAddress = Client.ClientAddress,
                                 ClientEmail = Client.ClientEmail,
                                 Departmentid = Client.Departmentid,
                                 DepartmentName = department.DepartmentName,
                                 Trainerid = Client.Trainerid,
                                 TrainerName = trainer.TrainerName,
                                 Subscriptionid = Client.Subscriptionid,
                                 SubscriptionName = subscription.SubscriptionName,
                                 Branchid = Client.Branchid,
                                 BranchName = branch.BranchName



                             }).ToList();
            return clients;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(Client user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Clients.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: /User/Login
        [AllowAnonymous] // Allow unauthenticated access to the login page
        public ActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [AllowAnonymous] // Allow unauthenticated access to the login action
        public ActionResult Login(string username, string password)
        {
            var user = _dbContext.Clients.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);

            if (user != null)
            {
                // Perform any necessary login actions (e.g., create an authentication cookie).
                return RedirectToAction("Index", "Home"); // Redirect to the home page after login.
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Departments = _dbContext.Departments.ToList();
            ViewBag.Trainers = _dbContext.Trainers.ToList();
            ViewBag.Subscriptions = _dbContext.Subscriptions.ToList();
            ViewBag.Branches = _dbContext.Branches.ToList();



            Client client = _dbContext.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                
                return View(client);
            }
        }
        [HttpPost]
        public IActionResult Edit(Client client)
        {
            
            if (ModelState.IsValid)
            {
                _dbContext.Entry(client).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Client client = _dbContext.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                
                return View(client);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            
            Client client = _dbContext.Clients.Find(id);
            if (client.Trainerid != null)
            {
                var trainer = _dbContext.Trainers.Find(client.Trainerid);
                if (trainer != null)
                {
                    trainer.Counter--;
                }
            }
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Client client = _dbContext.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                return View(client);
            }
        }
        [HttpPost]
        public IActionResult Details(Client client)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(client).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
        
    }
}
