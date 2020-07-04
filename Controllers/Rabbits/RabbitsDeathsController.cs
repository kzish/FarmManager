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
    [Route("RabbitsDeaths")]
    [Authorize(Roles = "admin")]
    public class RabbitsDeaths : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsDeathsController.";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsDeaths(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllDeadRabbits")]
        public IActionResult ajaxAllDeadRabbits()
        {
            ViewBag.title = "Rabbit Deaths Records";
            var rabbits = db.MRabbitsDeathsRecords.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }

        [HttpGet("AllDeadRabbits")]
        [HttpGet("")]
        public IActionResult AllDeadRabbits()
        {
            ViewBag.title = "Rabbit Deaths Records";
            return View();
        }

        [HttpGet("DeleteDeadRabbit/{id}")]
        public async Task<IActionResult> DeleteDeadRabbit(int id)
        {
            ViewBag.title = "Delete Rabbit";
            try
            {
                var Rabbit = db.MRabbitsDeathsRecords.Find(id);
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
            return RedirectToAction("AllDeadRabbits");
        }

        [HttpGet("CreateDeadRabbit")]
        public IActionResult CreateDeadRabbit()
        {
            ViewBag.title = "Create Rabbit";
            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var sold_rabbits = db.MRabbitsSalesRecords.Select(i => i.NameId).ToList();
            var available_rabbits = db.MRabbits
                .Where(i=>!dead_rabbits.Contains(i.NameId))
                .Where(i=>!sold_rabbits.Contains(i.NameId))
                .ToList();
            ViewBag.rabbits = available_rabbits;
            return View();
        }


        [HttpPost("CreateDeadRabbit")]
        public async Task<IActionResult> CreateDeadRabbit(MRabbitsDeathsRecords rabbit)
        {
            ViewBag.title = "Create Dead Rabbit";
            try
            {
                rabbit.NameId = rabbit.NameId.ToUpper();
                db.MRabbitsDeathsRecords.Add(rabbit);
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
                await db.SaveChangesAsync();
                return RedirectToAction("AllDeadRabbits");
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
                return RedirectToAction($"CreateDeadRabbit", "Rabbits");
            }
        }


        [HttpGet("EditDeadRabbit/{id}")]
        public IActionResult EditDeadRabbit(int id)
        {
            ViewBag.title = "Edit Rabbit";
            var rabbit = db.MRabbitsDeathsRecords.Find(id);
            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var sold_rabbits = db.MRabbitsSalesRecords.Select(i => i.NameId).ToList();
            var available_rabbits = db.MRabbits
                .Where(i => !dead_rabbits.Contains(i.NameId))
                .Where(i => !sold_rabbits.Contains(i.NameId))
                .ToList();
            ViewBag.rabbits = available_rabbits;
            ViewBag.rabbit = rabbit;
            return View();
        }


        [HttpPost("EditDeadRabbit")]
        public async Task<IActionResult> EditDeadRabbit(MRabbitsDeathsRecords rabbit, int old_rabbit_id)
        {
            ViewBag.title = "Edit Rabbit";
            try
            {
                var old_rabbit = db.MRabbitsDeathsRecords.Find(old_rabbit_id);
                old_rabbit.DateOfDeath = rabbit.DateOfDeath;
                old_rabbit.NameId = rabbit.NameId.ToUpper();
                old_rabbit.Notes = rabbit.Notes;
                db.Entry(old_rabbit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["msg"] = "Saved";
                TempData["type"] = "success";
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["type"] = "error";
            }
            return RedirectToAction("EditDeadRabbit", "RabbitsDeaths", new { id = old_rabbit_id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
