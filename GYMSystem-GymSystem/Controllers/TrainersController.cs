using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GYMSystem.Models;
using GYMSystem_GymSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace GYMSystem.Controllers
{
    [Authorize]

    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TrainersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private List<Trainer> GetTrainers()
        {
            var Trainers = (from Trainer in _dbContext.Trainers
                            select new Trainer
                            {
                                TrainerId = Trainer.TrainerId,
                                TrainerName = Trainer.TrainerName,
                                stillWork = Trainer.stillWork,
                                trainerSalary = Trainer.trainerSalary,
                                HiringDate = Trainer.HiringDate,
                                trainerNumber = Trainer.trainerNumber,
                                trainerAddress = Trainer.trainerAddress,
                                trainerEmail = Trainer.trainerEmail,
                                SubscriptionSalary = Trainer.SubscriptionSalary,
                                Counter = Trainer.Counter

                            }).ToList();
            return Trainers;

        }

        private DateTime DateTime { get; set; }

        public IActionResult Index()
        {
            var trainer = GetTrainers();
            return View(trainer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Branches = _dbContext.Branches.ToList();
            ViewBag.Departments = _dbContext.Departments.ToList();
            ViewBag.Subscriptions = _dbContext.Subscriptions.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Trainer trainer)
        {
            if (trainer.Counter == null)
            {
                trainer.Counter = 0;
            }

            if (ModelState.IsValid)
            {
                _dbContext.Trainers.Add(trainer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Trainer trainer = _dbContext.Trainers.Find(id);
                return View(trainer);
            }
        }
        [HttpPost]
        public IActionResult Edit(Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(trainer).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Trainer trainer = _dbContext.Trainers.Find(id);
                return View(trainer);
            }
        }
        [HttpPost]
        public IActionResult Delete(Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(trainer).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }




        public IActionResult Details(int id)
        {
            Trainer trainer = _dbContext.Trainers.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }
            else
            {
                return View(trainer);
            }
        }





        // Other code and methods...

       

    }
} 
    


