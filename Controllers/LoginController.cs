using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    public class LoginController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public LoginController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login(UserLogin login)
        {
            var user = await _context.UserLogins.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user != null)
            {
                if (user.Username != login.Username || user.Password != login.Password)
                {
                    ViewData["ValidateMessage"] = "Invalid username or Password";
                    return View();
                }

                if (user.IsAdmin == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }



            //Authenticate fail
            return View();
        }
    }
}
