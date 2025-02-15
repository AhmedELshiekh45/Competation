using AutoMapper;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.CompetitonBL.Dtos;

namespace ServiceLayer.CompetitonBL;

public class ComputionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private Competition compuition;

    public ComputionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        compuition = new Competition();
    }

    public async ValueTask Add(CreateCompetionDto entity)
    {
        compuition = _mapper.Map<Competition>(entity);
        await EnsurePartRegistrationHasData(entity.Date);
        await _unitOfWork.CompetitionRepo.CreateAsync(compuition);
        await _unitOfWork.Complete();

    }

    public async ValueTask Delete(string id)
    {
        await _unitOfWork.CompetitionRepo.DeleteAsync(id);
        await _unitOfWork.Complete();

    }

    public async Task EnsurePartRegistrationHasData(DateOnly date)
    {
        // Check if the table is empty
        if (!_unitOfWork._Context.Competitions.Any(p => p.Date == date))
        {
            var partRegistrations = new List<PartRegistration>
                {
             new PartRegistration {Id=$"{date}-1", NumberOfParts = 1, SerialNumber = 0, ExamDate = date },
            new PartRegistration { Id=$"{date}-2", NumberOfParts = 2, SerialNumber = 0, ExamDate = date },
            new PartRegistration { Id=$"{date}-3", NumberOfParts = 3, SerialNumber = 0, ExamDate = date },
            new PartRegistration {Id = $"{date}-4",  NumberOfParts = 4, SerialNumber = 0, ExamDate = date },
            new PartRegistration {Id = $"{date}-5", NumberOfParts = 5, SerialNumber = 0, ExamDate = date},
            new PartRegistration {Id = $"{date}-7.5",  NumberOfParts = 7.5f, SerialNumber = 0, ExamDate = date },
            new PartRegistration {Id = $"{date}-10", NumberOfParts = 10, SerialNumber = 0, ExamDate = date},
            new PartRegistration {Id=$"{date}-15", NumberOfParts = 15, SerialNumber = 0, ExamDate = date},
            new PartRegistration {Id = $"{date}-20", NumberOfParts = 20, SerialNumber = 0, ExamDate = date},
            new PartRegistration {Id = $"{date}-30", NumberOfParts = 30, SerialNumber = 0, ExamDate = date}
             };

            _unitOfWork._Context.PartRegistrations.AddRange(partRegistrations);
            await _unitOfWork.Complete();
        }
    }

    public async ValueTask Edit(CreateCompetionDto entity)
    {
        compuition = await _unitOfWork.CompetitionRepo.GetByIdAsync(entity.Id);
        compuition = _mapper.Map<Competition>(entity);
        await _unitOfWork.Complete();
    }

    public async ValueTask<IEnumerable<CompetaionDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<CompetaionDto>>(await _unitOfWork.CompetitionRepo.GetWithQuery(p=>p.Include(p=>p.Exams)));
    }

    public async ValueTask<CreateCompetionDto> GetById(string id)
    {
        return _mapper.Map<CreateCompetionDto>(await _unitOfWork.CompetitionRepo.GetByIdAsync(id));
    }

    public ValueTask<CreateCompetionDto> GetByName(string name)
    {
        throw new NotImplementedException();
    }
}
