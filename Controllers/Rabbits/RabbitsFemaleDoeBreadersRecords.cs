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
    [Route("RabbitsFemaleDoeBreadersRecords")]
    [Authorize(Roles = "admin")]
    public class RabbitsFemaleDoeBreadersRecords : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsFemaleDoeBreadersRecords";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsFemaleDoeBreadersRecords(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllRabbits")]
        public IActionResult ajaxAllRabbits()
        {
            ViewBag.title = "Rabbit Breaders Records";
            var rabbits = db.MRabbitsFemaleDoeBreadersRecords.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }

        [HttpGet("AllRabbits")]
        [HttpGet("")]
        public IActionResult AllRabbits()
        {
            ViewBag.title = "Rabbit Breaders Records";
            return View();
        }

        [HttpGet("DeleteRabbit/{id}")]
        public async Task<IActionResult> DeleteRabbit(int id)
        {
            ViewBag.title = "Delete Rabbit";
            try
            {
                var Rabbit = db.MRabbitsFemaleDoeBreadersRecords.Find(id);
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

        [HttpGet("CreateRabbit")]
        public IActionResult CreateRabbit()
        {
            ViewBag.title = "Create Rabbit";

            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var sold_rabbits = db.MRabbitsSalesRecords.Select(i => i.NameId).ToList();
            var available_rabbits = db.MRabbits
                .Where(i => !dead_rabbits.Contains(i.NameId))
                .Where(i => !sold_rabbits.Contains(i.NameId))
                .ToList();
            ViewBag.rabbits = available_rabbits;

            return View();
        }


        [HttpPost("CreateRabbit")]
        public async Task<IActionResult> CreateRabbit(MRabbitsFemaleDoeBreadersRecords rabbit)
        {
            ViewBag.title = "Create Rabbit";
            try
            {
                rabbit.NameIdBuck = rabbit.NameIdBuck.ToUpper();
                rabbit.NameIdDoe = rabbit.NameIdDoe.ToUpper();
                db.MRabbitsFemaleDoeBreadersRecords.Add(rabbit);
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
            }
            return RedirectToAction("AllRabbits");

        }


        [HttpGet("EditRabbit/{id}")]
        public IActionResult EditRabbit(int id)
        {
            ViewBag.title = "Edit Rabbit";
            var rabbit = db.MRabbitsFemaleDoeBreadersRecords.Find(id);

            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var sold_rabbits = db.MRabbitsSalesRecords.Select(i => i.NameId).ToList();
            var available_rabbits = db.MRabbits
                .Where(i => !dead_rabbits.Contains(i.NameId))
                .Where(i => !sold_rabbits.Contains(i.NameId))
                .ToList();
           

            ViewBag.rabbit = rabbit;
            ViewBag.rabbits = available_rabbits;
            return View();
        }


        [HttpPost("EditRabbit")]
        public async Task<IActionResult> EditRabbit(MRabbitsFemaleDoeBreadersRecords rabbit)
        {
            ViewBag.title = "Edit Rabbit";
            try
            {
                rabbit.NameIdBuck = rabbit.NameIdBuck.ToUpper();
                rabbit.NameIdDoe = rabbit.NameIdDoe.ToUpper();
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
            return RedirectToAction("EditRabbit", "RabbitsFemaleDoeBreadersRecords", new { id = rabbit.Id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
