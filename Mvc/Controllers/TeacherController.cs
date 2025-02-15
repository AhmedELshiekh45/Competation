using DataAccess_Layer.DTOS;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.TeacherLogic.Service;

namespace Presentation.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherService _services;

        public TeacherController(TeacherService services)
        {

            this._services = services;

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAll();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await _services.GetTeacherExams(id);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateTeacherDto dto)
        {
            if (ModelState.IsValid)
            {
                await _services.Add(dto);
                return RedirectToAction(nameof(Add), dto);
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> PrintTheForm(CreateTeacherDto contestantDto)
        {
            return View(contestantDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _services.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CreateTeacherDto dto)
        {
            if (ModelState.IsValid)
            {
                await _services.Edit(dto);
                return RedirectToAction(nameof(GetAll));
            }

            return View(dto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _services.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
    }
}
