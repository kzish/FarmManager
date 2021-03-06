﻿using System;
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
    /// general notes about rabbits and the rabbitry
    /// </summary>
    [Route("RabbitsDeseasesAndTreatments")]
    [Authorize(Roles = "admin")]
    public class RabbitsDeseasesAndTreatmentsController : Controller
    {
        dbContext db = new dbContext();
        private string source = "FarmManager.Controllers.RabbitsDeseasesAndTreatmentsController.";
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public RabbitsDeseasesAndTreatmentsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("ajaxAllRecords")]
        public IActionResult ajaxAllRecords()
        {
            ViewBag.title = "All Rabbitry Deseases";
            var rabbits = db.MRabbitsDeseasesAndTreatmentNotes.ToList();
            ViewBag.rabbits = rabbits;
            return View();
        }

        [HttpGet("AllRecords")]
        [HttpGet("")]
        public IActionResult AllRecords()
        {
            ViewBag.title = "All Rabbitry Deseases";
            return View();
        }

        [HttpGet("DeleteRecord/{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            ViewBag.title = "Delete Record";
            try
            {
                var Rabbit = db.MRabbitsDeseasesAndTreatmentNotes.Find(id);
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
            return View();
        }


        [HttpPost("CreateRecord")]
        public async Task<IActionResult> CreateRecord(MRabbitsDeseasesAndTreatmentNotes rabbit)
        {
            ViewBag.title = "Create Record";
            try
            {
                rabbit.Desease = rabbit.Desease.ToUpper();
                db.MRabbitsDeseasesAndTreatmentNotes.Add(rabbit);
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
            var record = db.MRabbitsDeseasesAndTreatmentNotes.Find(id);
            ViewBag.record = record;
            return View();
        }


        [HttpPost("EditRecord")]
        public async Task<IActionResult> EditRecord(MRabbitsDeseasesAndTreatmentNotes rabbit)
        {
            ViewBag.title = "Edit Record";
            try
            {
                rabbit.Desease = rabbit.Desease.ToUpper();
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
            return RedirectToAction("AllRecords");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
