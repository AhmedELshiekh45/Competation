using AutoMapper;
using DataAccess_Layer.Context;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ContestantBL.Dtos;

namespace ServiceLayer.ContestantBL.Service
{
    public class ContestentServices
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContestentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool DoesContestantExistByPhoneNumber(string id)
        {
            return _unitOfWork._Context.Contestants.Any(c => c.Id == id);
        }
        public async ValueTask<PrintContestantDto> Add(CreateContestantDto Dto)
        {
            if (DoesContestantExistByPhoneNumber(Dto.Id))
            {
                var comp = await _unitOfWork.CompetitionRepo.GetByIdAsync(Dto.ComputaionId);
                var serial = await _unitOfWork.ContestantRepo.UpdateSerialNumberAndDateTime($"{comp.Date}-{Dto.PartsNumber}");
                var contestants = await _unitOfWork.ContestantRepo.GetWithQuery(p => p.Include(p => p.Exams));
                var contestant = contestants.FirstOrDefault(p => p.PhoneNumber == Dto.PhoneNumber);
                contestant.Exams.Add(
                    new Exam
                    {
                        SerialNumber = serial,
                        CompetitionId = Dto.ComputaionId,
                        TeacherId = Dto.TeacherId,
                        TotalScore = 0,
                        PartsNumber = Dto.PartsNumber,
                         EntryId=Dto.EntryId

                    }
                    );
                await _unitOfWork.Complete();
                var print = _mapper.Map<PrintContestantDto>(Dto);
                print.SerialNumber = serial;
                print.CompetationDate = comp.Date;
               

                return print;
            }
            var entity = _mapper.Map<Contestant>(Dto);
            await _unitOfWork.ContestantRepo.CreateAsync(entity);
            var competaion = await _unitOfWork.CompetitionRepo.GetByIdAsync(Dto.ComputaionId);

            var serialnumber = await _unitOfWork.ContestantRepo.UpdateSerialNumberAndDateTime($"{competaion.Date}-{Dto.PartsNumber}");
            entity.Exams.LastOrDefault().SerialNumber = serialnumber;
            await _unitOfWork.Complete();
            var printdto = _mapper.Map<PrintContestantDto>(Dto);
            printdto.SerialNumber = serialnumber;
            printdto.CompetationDate = competaion.Date;
            return printdto;
        }

        private ICollection<Question> CreateExamQuestions(string id)
        {
            var questions = new List<Question>();
            for (int i = 1; i < 11; i++)
            {
                questions.Add(new Question { Name = $"السؤال {i}", ExamId = id, Score = 0 });
            }
            return questions;
        }

        public async ValueTask Delete(string id)
        {
            await _unitOfWork.ContestantRepo.DeleteAsync(id);
            await _unitOfWork.Complete();

        }

        public async ValueTask Edit(CreateContestantDto Dto)
        {
            var x = await _unitOfWork.ContestantRepo.GetByIdAsync(Dto.Id);
            x.PhoneNumber = Dto.PhoneNumber;
            x.TeacherId = Dto.TeacherId;
            x.Id = Dto.Id;
            x.BirthDate = Dto.BirthDate;
            x.FullName = Dto.FullName;
            x.Education = Dto.Education;

            await _unitOfWork.Complete();
        }
        public async ValueTask<ContestantDetailsDto> GetById(string id)
        {
            var query = await _unitOfWork.ContestantRepo.GetWithQuery(p => p
                 .Include(c => c.Exams)
                 .ThenInclude(e => e.Competition) // Include Compuition (if it's a navigation property)
                 .Include(c => c.Exams)
                  .ThenInclude(e => e.Competition)
                   );
            var entity = _mapper.Map<ContestantDetailsDto>(query.FirstOrDefault(p => p.Id == id));
            return entity;
        }

        public async ValueTask<CreateContestantDto> GetByIdAsync(string id)
        {
            var query = await _unitOfWork.ContestantRepo.GetWithQuery(p =>
                p.Include(c => c.Exams)
                 .ThenInclude(e => e.Competition) // Include Compuition (if it's a navigation property)
            ); var entity = _mapper.Map<CreateContestantDto>(await query.FirstOrDefaultAsync(p => p.Id == id));
            return entity;
        }
        public ValueTask<CreateContestantDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async ValueTask< IEnumerable<GetallContestantsDto>> GetContestantsByDate(DateTime competitionDate)
        {

            var contestants = _unitOfWork._Context.Competitions
                .Where(c => c.Exams.Any(e => e.Competition.Date.Equals(competitionDate)))
                .ToList();

            return _mapper.Map<IEnumerable<GetallContestantsDto>>(contestants);
        }
        public async ValueTask<IEnumerable<GetallContestantsDto>> GetAll(string teacherid=null)
        {

            if (teacherid != null)
            {
                var q = await _unitOfWork.ContestantRepo.GetWithQuery(p => p
                    .Include(c => c.Exams).ThenInclude(p => p.Competition));
                var res = await q.Where(p => p.Exams.OrderByDescending(p=>p.CreatedAt).FirstOrDefault().TeacherId == teacherid).ToListAsync();
                // Filter contestants by the teacher ID of their last exam
                return _mapper.Map<IEnumerable<GetallContestantsDto>>(res); // Map to DTO
            }
            var query = await _unitOfWork.ContestantRepo.GetWithQuery(p => p.Include(c => c.Exams).ThenInclude(p => p.Competition));
            var result = await query.ToListAsync();

            return _mapper.Map<IEnumerable<GetallContestantsDto>>(result);
        }




    }
}
