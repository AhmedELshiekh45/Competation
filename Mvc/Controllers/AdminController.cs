using System.Security.Claims;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUnitOfWork unitOfWork;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager,IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task< IActionResult> DashBord()
        {
            ViewBag.Contestants=unitOfWork._Context.Contestants.Distinct().Count();
            ViewBag.Teachers=unitOfWork._Context.Teachers.Distinct().Count();
            ViewBag.Exams=unitOfWork._Context.Exams.Count();
            var userid = (User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
            var result = new List<UserDto>();
            var users = await userManager.Users.ToListAsync();
            foreach (var item in users )
            {
                var roles = await userManager.GetRolesAsync(item); // جلب الأدوار الخاصة بالمستخدم
                result.Add(new UserDto
                { Id=item.Id,
                    UserName = item.UserName,
                    Role = roles.FirstOrDefault(), 
                    NumberOfContestants=unitOfWork._Context.Exams.Where(p=>p.EntryId==item.Id).Count()// إذا كان هناك دور واحد فقط، نأخذ أول دور
                });
            }




            return View(result);
        }


        [HttpGet]
        public async Task< IActionResult> Delete(string username)
        {
            var x = await userManager.FindByNameAsync(username);
            await userManager.DeleteAsync(x);
            return RedirectToAction("DashBord");
        }

    }
}
