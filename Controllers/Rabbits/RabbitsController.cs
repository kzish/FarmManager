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
    [Route("Rabbits")]
    [Route("")]
    [Authorize(Roles = "admin")]
    public class RabbitsController : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsController.";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllRabbits")]
        public IActionResult ajaxAllRabbits()
        {
            ViewBag.title = "All Rabbits";
            var rabbits = db.MRabbits.ToList();
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
                var Rabbit = db.MRabbits.Find(id);
                db.Remove(Rabbit);
                await db.SaveChangesAsync();
                TempData["type"] = "success";
                TempData["msg"] = "Deleted";
                return RedirectToAction("AllRabbits");
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet("CreateRabbit")]
        public IActionResult CreateRabbit()
        {
            ViewBag.title = "Create Rabbit";
            var rabbits = db.MRabbits.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }


        [HttpPost("CreateRabbit")]
        public async Task<IActionResult> CreateRabbit(MRabbits rabbit)
        {
            ViewBag.title = "Create Rabbit";
            try
            {
                rabbit.NameId = rabbit.NameId.ToUpper();
                rabbit.SireId = rabbit.SireId.ToUpper();
                rabbit.DameId = rabbit.DameId.ToUpper();
                db.MRabbits.Add(rabbit);
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
            var rabbit = db.MRabbits.Find(id);
            var rabbits = db.MRabbits.ToList();
            ViewBag.rabbit = rabbit;
            ViewBag.rabbits = rabbits;
            return View();
        }


        [HttpPost("EditRabbit")]
        public async Task<IActionResult> EditRabbit(MRabbits rabbit, int old_rabbit_id)
        {
            ViewBag.title = "Edit Rabbit";
            try
            {
                var old_rabbit = db.MRabbits.Find(old_rabbit_id);
                old_rabbit.DateOfBirth = rabbit.DateOfBirth;
                old_rabbit.NameId = rabbit.NameId.ToUpper();
                old_rabbit.SireId = rabbit.SireId.ToUpper();
                old_rabbit.DameId = rabbit.DameId.ToUpper();
                old_rabbit.LitterNumber = rabbit.LitterNumber;
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
            return RedirectToAction("EditRabbit", "Rabbits", new { id = old_rabbit_id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
