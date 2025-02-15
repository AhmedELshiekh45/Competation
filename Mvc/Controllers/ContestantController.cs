using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess_Layer.Context;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ContestantBL.Dtos;
using ServiceLayer.ContestantBL.Service;

namespace Presentation.Controllers
{
    public class ContestantController : Controller
    {

        private readonly ContestentServices _services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private Contestant contestant;

        public ContestantController(ContestentServices services, IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {

            this._services = services;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
            contestant = new Contestant();
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> GetAllContestant(string teacherid=null)
        {
            var result = await _services.GetAll(teacherid);

            var Competaions = await _unitOfWork.CompetitionRepo.GetWithQuery(p => p.Include(s => s.Exams));
            ViewBag.CompetationDate = Competaions.OrderBy(p => p.Date).LastOrDefault().Date;
            ViewBag.Teachers = await _unitOfWork.TeacherRepo.GetAllAsync();
            return View(result);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await _services.GetById(id);
            return View(result);
        }
        [Authorize(Roles = "Admin,Data Entry")]


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var teachers = await _unitOfWork.TeacherRepo.GetAllAsync();
            ViewBag.Teachers = teachers.ToList();
            var computaion = await _unitOfWork.CompetitionRepo.GetAllAsync();
            ViewBag.Competations = computaion.OrderByDescending(p => p.Date).ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Data Entry")]


        [HttpPost]
        public async Task<IActionResult> Add(CreateContestantDto dto)
        {
            if (ModelState.IsValid)
            {
                dto.EntryId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
                var print = await _services.Add(dto);
                return RedirectToAction("PrintTheForm", print);
            }
            return View(dto);
        }
      

        [Authorize(Roles = "Admin,Data Entry")]

        [HttpGet]
        public async Task<IActionResult> PrintTheForm(PrintContestantDto dto)
        {
            var x = await _unitOfWork.TeacherRepo.GetByIdAsync(dto.TeacherId);
            dto.TeacherId = x.Name;
            return View(dto);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(string id)
        {
            var teachers = await _unitOfWork.TeacherRepo.GetAllAsync();
            ViewBag.Teachers = teachers.ToList();
            ViewBag.ContestantId = id;
            var computaion = await _unitOfWork.CompetitionRepo.GetAllAsync();
            ViewBag.Computaion = await computaion.FirstOrDefaultAsync(p => p.Date.Year == DateTime.Now.Year);
            return View(await _services.GetByIdAsync(id));
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(CreateContestantDto dto)
        {
            if (ModelState.IsValid)
            {
                await _services.Edit(dto);
                return RedirectToAction(nameof(GetAllContestant));
            }

            return View(dto);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(string id)
        {
            await _services.Delete(id);
            return RedirectToAction(nameof(GetAllContestant));
        }
    }
}
