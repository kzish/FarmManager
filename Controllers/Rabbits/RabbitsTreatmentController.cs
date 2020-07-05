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
    /// <summary>
    /// deals with the rabbits treatments
    /// </summary>
    [Route("RabbitsTreatment")]
    [Authorize(Roles = "admin")]
    public class RabbitsTreatmentController : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsTreatmentController.";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsTreatmentController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllRecords")]
        public IActionResult ajaxAllRecords()
        {
            ViewBag.title = "All Rabbit Treatments";
            var rabbits = db.MRabbitsTreatmentRecords.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }

        [HttpGet("AllRecords")]
        [HttpGet("")]
        public IActionResult AllRecords()
        {
            ViewBag.title = "All Rabbit Treatments";
            return View();
        }

        [HttpGet("DeleteRecord/{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            ViewBag.title = "Delete Record";
            try
            {
                var Rabbit = db.MRabbitsTreatmentRecords.Find(id);
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
            return RedirectToAction("AllRecords");
        }

        [HttpGet("CreateRecord")]
        public IActionResult CreateRecord()
        {
            ViewBag.title = "Create Record";
            var dead_rabbits = db.MRabbitsDeathsRecords.Select(i => i.NameId).ToList();
            var sold_rabbits = db.MRabbitsSalesRecords.Select(i => i.NameId).ToList();
            var available_rabbits = db.MRabbits
                .Where(i => !dead_rabbits.Contains(i.NameId))
                .Where(i => !sold_rabbits.Contains(i.NameId))
                .ToList();
            ViewBag.rabbits = available_rabbits;
            return View();
        }


        [HttpPost("CreateRecord")]
        public async Task<IActionResult> CreateRabbitSaleRecord(MRabbitsTreatmentRecords rabbit)
        {
            ViewBag.title = "Create Record";
            try
            {
                rabbit.NameId = rabbit.NameId.ToUpper();
                db.MRabbitsTreatmentRecords.Add(rabbit);
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = "Error: " + ex.Message;
                
            }
            return RedirectToAction("AllRecords");
        }


        [HttpGet("EditRecord/{id}")]
        public IActionResult EditRecord(int id)
        {
            ViewBag.title = "Edit Record";
            var rabbit = db.MRabbitsTreatmentRecords.Find(id);
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


        [HttpPost("EditRecord")]
        public async Task<IActionResult> EditRecord(MRabbitsTreatmentRecords rabbit)
        {
            ViewBag.title = "Edit Record";
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
            return RedirectToAction("EditRecord", "RabbitsTreatment", new { id = rabbit.Id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
