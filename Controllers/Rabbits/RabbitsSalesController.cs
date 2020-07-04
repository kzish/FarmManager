using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FarmManager.Controllers
{
    [Route("RabbitsSales")]
    [Authorize(Roles = "admin")]
    public class RabbitsSalesController : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsController.";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsSalesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllRabbits")]
        public IActionResult ajaxAllRabbits()
        {
            ViewBag.title = "All Rabbits";
            var rabbits = db.MRabbitsSalesRecords.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }

        [HttpGet("AllRabbits")]
        [HttpGet("")]
        public IActionResult AllRabbits()
        {
            ViewBag.title = "All Rabbits";
            return View();
        }

        [HttpGet("DeleteRabbit/{id}")]
        public async Task<IActionResult> DeleteRabbit(int id)
        {
            ViewBag.title = "Delete Rabbit";
            try
            {
                var Rabbit = db.MRabbitsSalesRecords.Find(id);
                db.Remove(Rabbit);
                await db.SaveChangesAsync();
                TempData["type"] = "success";
                TempData["msg"] = "Deleted";
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
            }
            return RedirectToAction("AllRabbits");
        }

        [HttpGet("CreateRabbitSaleRecord")]
        public IActionResult CreateRabbitSaleRecord()
        {
            ViewBag.title = "Create Rabbit";



            //var rabbits = db.MRabbits.Where(i=>i.NameId).ToList();
            //ViewBag.rabbits = rabbits;
            return View();
        }


        [HttpPost("CreateRabbitSaleRecord")]
        public async Task<IActionResult> CreateRabbitSaleRecord(MRabbitsSalesRecords rabbit)
        {
            ViewBag.title = "Create Rabbit";
            try
            {
                rabbit.NameId = rabbit.NameId.ToUpper();
                db.MRabbitsSalesRecords.Add(rabbit);
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
                await db.SaveChangesAsync();
                return RedirectToAction("AllRabbits");
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
                return RedirectToAction($"CreateRabbit", "Rabbits");
            }
        }


        [HttpGet("EditRabbit/{id}")]
        public IActionResult EditRabbit(int id)
        {
            ViewBag.title = "Edit Rabbit";
            var rabbit = db.MRabbitsSalesRecords.Find(id);
            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var rabbits_that_are_not_dead = db.MRabbits.Where(i => !dead_rabbits.Contains(i.NameId)).ToList();
            ViewBag.rabbit = rabbit;
            ViewBag.rabbits = rabbits_that_are_not_dead;
            return View();
        }


        [HttpPost("EditRabbit")]
        public async Task<IActionResult> EditRabbit(MRabbitsSalesRecords rabbit, int old_rabbit_id)
        {
            ViewBag.title = "Edit Rabbit";
            try
            {
                db.Entry(rabbit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["msg"] = "Saved";
                TempData["type"] = "success";
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["type"] = "error";
            }
            return RedirectToAction("EditRabbit", "Rabbits", new { id = old_rabbit_id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
