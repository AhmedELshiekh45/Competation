using DataAccess_Layer.DTOS;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ExamLogic.Dtos;
using ServiceLayer.ExamLogic.Service;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ExamController : Controller
    {

        private readonly ExamService _services;
        private readonly IUnitOfWork _unitOfWork;
        private Exam Exam;

        public ExamController(ExamService services, IUnitOfWork unit)
        {

            this._services = services;
            this._unitOfWork = unit;
            Exam = new Exam();
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContestant()
        {
            var result = await _services.GetAll();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(string id, string contestantid)
        {
            var result = await _services.GetById(id);
            result.ContestantId = contestantid;
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ExamDto dto)
        {
            if (ModelState.IsValid)
            {
                await _services.Add(dto);
                return RedirectToAction(nameof(Add));
            }
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id, string contestantId)
        {
            var exam = await _services.GetById(id);
            exam.ContestantId = contestantId;
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExamDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // جلب الامتحان من قاعدة البيانات

            await _services.Edit(model);
            return RedirectToAction(nameof(GetDetails), "Contestant", new { id = model.ContestantId });
        }

        public async Task<IActionResult> Delete(string id, string contestantId)
        {
            await _services.Delete(id);
            return RedirectToAction(nameof(GetDetails), "Contestant", new { id = contestantId });
        }
    }
}
