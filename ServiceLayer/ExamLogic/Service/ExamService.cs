using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ContestantBL.Dtos;
using ServiceLayer.ExamLogic.Dtos;

namespace ServiceLayer.ExamLogic.Service
{
    public class ExamService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ExamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async ValueTask Add(ExamDto Dto)
        {
            var entity = _mapper.Map<Exam>(Dto);

            await _unitOfWork.ExamRepo.CreateAsync(entity);
            await _unitOfWork.Complete();
        }

        public async ValueTask Delete(string id)
        {
            await _unitOfWork.ExamRepo.DeleteAsync(id);
            await _unitOfWork.Complete();

        }

        public async ValueTask Edit(ExamDto Dto)
        {
            var x = await GetByIdAsync(Dto.Id);
            if (x==null)
            {
                return;
            }
            x.TotalScore = Dto.TotalScore;
        
            await _unitOfWork.Complete();
        }
        //public async ValueTask<ExamDto> EditQuestions(string Examid)
        //{
        //    var query = await _unitOfWork.ExamRepo.GetWithQuery(p => p.Include(s => s.Questions));
        //    var result = await query.FirstOrDefaultAsync(p => p.Id == Examid);
        //    return _mapper.Map<ExamDto>(result);
        //}
        public async ValueTask<ExamDetailsDto> GetById(string id)
        {
            var query = await _unitOfWork.ExamRepo.GetWithQuery(p =>
                p.Include(x => x.Teacher)
                 .Include(e => e.Competition).Include(p => p.Contestant) // Include Compuition (if it's a navigation property)
            ); var entity = _mapper.Map<ExamDetailsDto>(await query.FirstOrDefaultAsync(p => p.Id == id));
            return entity;
        }
        public async ValueTask<Exam> GetByIdAsync(string id)
        {
            var query = await _unitOfWork.ExamRepo.GetWithQuery(p =>
                p.Include(p=>p.Teacher)
                 .Include(e => e.Competition).Include(p => p.Contestant) // Include Compuition (if it's a navigation property)
            );
            var entity = await query.FirstOrDefaultAsync(p => p.Id == id);
            return entity;
        }

        public ValueTask<CreateContestantDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<IEnumerable<ExamDto>> GetAll()
        {
            var query = await _unitOfWork.ExamRepo.GetWithQuery(p => p.Include(p => p.Competition).Include(p=>p.Teacher));
            var result = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ExamDto>>(result);
        }

        public static ICollection<Question> CreateExamQuestions(string id)
        {
            var questions = new List<Question>();
            for (int i = 1; i < 11; i++)
            {
                questions.Add(new Question { Name = $"السؤال {i}", ExamId = id, Score = 0 });
            }
            return questions;
        }

    }
}
