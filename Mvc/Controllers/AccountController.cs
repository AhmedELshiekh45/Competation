using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using AutoMapper;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Constants;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AccountController : Controller

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private User user;

        public AccountController(IUnitOfWork unitOfWork, SignInManager<User> signInManager,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this.user = new User();
        }
        [HttpGet]
        public async Task<IActionResult> RegisterAdmin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO registerDto)
        {
            if (ModelState.IsValid)
            {
                user=_mapper.Map<User>(registerDto);
                var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.PasswordHash);
                if (result.Succeeded)
                {
                    var found = await _unitOfWork.UserManager.AddToRoleAsync(user, "Admin");
                    var claim = new Claim("Role", "Admin");
                    await _unitOfWork.UserManager.AddClaimAsync(user, claim);
                    if (found.Succeeded)
                    {

                        return RedirectToAction("LogIn"); //new GenralResponce { IsSuccess = true, Data = "Created Succesfuly" };
                    }
                    else
                    {
                        foreach (var item in found.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(registerDto);

        }
        [HttpGet]
        public async Task<ActionResult<GenralResponce>> RegisterDataEntry()
        {
            return View();
        }

            [HttpPost]
        public async Task<ActionResult<GenralResponce>> RegisterDataEntry(RegisterDTO registerDto)
        {
            if (ModelState.IsValid)
            {
                user = _mapper.Map<User>(registerDto);
                var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.PasswordHash);
                if (result.Succeeded)
                {
                    var found = await _unitOfWork.UserManager.AddToRoleAsync(user, "Data Entry");
                    var claim = new Claim("Role", "Data Entry");
                    await _unitOfWork.UserManager.AddClaimAsync(user, claim);
                    if (found.Succeeded)
                    {
                        return RedirectToAction("LogIn");
                    }
                    else
                    {
                        foreach (var item in found.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(registerDto);

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            if (ModelState.IsValid)
            {
                user = await _unitOfWork.UserManager.FindByEmailAsync(logInDto.Email);
                if (user != null)
                {
                    var found = await _unitOfWork.UserManager.CheckPasswordAsync(user, logInDto.Password);
                    if (found)
                    {
                       var adminRole = await _unitOfWork.UserManager.IsInRoleAsync(user, "Admin");
                        if (adminRole)
                        {
                            await _signInManager.SignInAsync(user, logInDto.RememberMe);
                            return RedirectToAction("DashBord", "Admin");
                        }
                        await _signInManager.SignInAsync(user, logInDto.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not Found User");
                    }
                }
                ModelState.AddModelError("", "UserName Or Password Incorrect");
            }
            return View(logInDto);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home");
        }





    }
}

