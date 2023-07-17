using DATA.Models;
using DBFirst.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DBFirst.Controllers
{
    public class AccountController : Controller
    {
        FinalDBCotext db = new FinalDBCotext();
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.Key = TempData["Key"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            FinalDBCotext cont = new FinalDBCotext();
            var data = cont.Users.Where(e => e.Username == model.Email).FirstOrDefault();
            if (data!=null)
            {
                bool isValid = (data.Username == model.Email && DecryptPassword(data.Password) == model.PassWord);
                if (isValid)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.Email),
                };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimIdentity), authProperties);
                    TempData["Key"] = "Login Successfull";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Key"] = "Wrong information enter again";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SignUpVM log)
        {
            var User1 = new Users()
            {
                Id = log.UserId,
                Username = log.Email,
                Password = EncryptPassword(log.PassWord)
            };
            db.Users.Add(User1);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storepassword = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
                string encryptPassword = Convert.ToBase64String(storepassword);
                return encryptPassword;
            }
        }
        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptPassword;
            }
        }
    }
}
