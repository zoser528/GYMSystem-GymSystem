using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GYMSystem_GymSystem.Data;
using GYMSystem_GymSystem.Areas.Admin.Models;

namespace GYMSystem_GymSystem.Areas.Admin.Controllers
{
    [Authorize("AdminRole")]
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