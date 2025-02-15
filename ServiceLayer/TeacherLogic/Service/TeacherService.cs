using AutoMapper;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.TeacherLogic.Dtos;

namespace ServiceLayer.TeacherLogic.Service
{
    public class TeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Teacher Teacher;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Teacher = new Teacher();
        }

        public async ValueTask Add(CreateTeacherDto entity)
        {
            Teacher.Name = entity.Name;
            await _unitOfWork.TeacherRepo.CreateAsync(Teacher);
            await _unitOfWork.Complete();

        }

        public async ValueTask Delete(string id)
        {
            await _unitOfWork.TeacherRepo.DeleteAsync(id);
            await _unitOfWork.Complete();

        }
        public async ValueTask Edit(CreateTeacherDto entity)
        {
            Teacher = await _unitOfWork.TeacherRepo.GetByIdAsync(entity.Id);
            Teacher = _mapper.Map<Teacher>(entity);
            await _unitOfWork.Complete();
        }

        public async ValueTask<IEnumerable<TeacherDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _unitOfWork.TeacherRepo.GetWithQuery(p => p.Include(s => s.ContestantsExams)));
        }

        public async ValueTask<CreateTeacherDto> GetById(string id)
        {
            return _mapper.Map<CreateTeacherDto>(await _unitOfWork.TeacherRepo.GetByIdAsync(id));
        }
        public async ValueTask<IEnumerable<Exam>> GetTeacherExams(string id)
        {
             var query =await _unitOfWork.ExamRepo.GetWithQuery(
                p => p.Include(p => p.Contestant)
                .Include(p => p.Competition));
            var result = await query.Where(p => p.TeacherId == id).ToListAsync();
            return result;
        }

        public ValueTask<CreateTeacherDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }


    }
}
