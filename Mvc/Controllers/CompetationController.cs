using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.CompetitonBL;
using ServiceLayer.CompetitonBL.Dtos;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompetationController : Controller
    {

        private readonly ComputionService _services;
        private Contestant contestant;

        public CompetationController(ComputionService services)
        {

            this._services = services;

            contestant = new Contestant();
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
            var result = await _services.GetById(id);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCompetionDto dto)
        {
            if (ModelState.IsValid)
            {
                await _services.Add(dto);
                return RedirectToAction(nameof(Add), dto);
            }
            return View(dto);
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _services.GetById(id));
        }

        public async Task<IActionResult> Edit(CreateCompetionDto dto)
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
