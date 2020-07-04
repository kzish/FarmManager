using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FarmManager.Models;

namespace admin.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        dbContext db = new dbContext();
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// add admin user incase non exists
        /// </summary>
        private async Task<string> seedDb()
        {
            var admin_user = db.AspNetUsers.Where(i=>i.Email=="admin@rubiem.com").FirstOrDefault();
            if (admin_user==null)
            {
                var new_admin_user = new IdentityUser();
                new_admin_user.Email = "admin@rubiem.com";
                new_admin_user.UserName = new_admin_user.Email;
                await userManager.CreateAsync(new_admin_user, "Rubiem#99");//create user
                
                bool x = await roleManager.RoleExistsAsync("admin");
                if (!x)
                {
                    var role = new IdentityRole("admin");
                    await roleManager.CreateAsync(role);
                }
                await userManager.AddToRoleAsync(new_admin_user, "admin");//add user to admin role
            }
            return "ok";
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            ViewBag.title = "Login";
            await seedDb();
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password, string ReturnUrl)
        {
            ViewBag.title = "Login";
            var id_user = new IdentityUser { UserName = email, Email = email };
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {

                //signin as a company and redirect
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl) && ReturnUrl != "%2" && ReturnUrl != "/")
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            else
            {
                TempData["type"] = "error";
                TempData["msg"] = "Invalid credentials";
                TempData["email"] = email;
                return View();
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("LogOff")]
        public async Task<ActionResult> LogOff()
        {
            ViewBag.title = "Log Off";
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
