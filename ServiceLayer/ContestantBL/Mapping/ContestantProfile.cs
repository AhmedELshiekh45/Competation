using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_Layer.Models;
using ServiceLayer.ContestantBL.Dtos;

namespace ServiceLayer.ContestantBL.Mapping
{
    public class ContestantProfile : Profile
    {
        public ContestantProfile()
        {
            CreateMap<Contestant, CreateContestantDto>()
                .ForMember(p => p.TeacherId, c => c.MapFrom(p => p.Exams.LastOrDefault().TeacherId))
                .ForMember(p => p.TeacherId, c => c.MapFrom(p => p.Exams.LastOrDefault().TeacherId))
                .ForMember(p => p.Score, c => c.MapFrom(p => p.Exams.LastOrDefault().TotalScore));

            CreateMap<CreateContestantDto, Contestant>()
         
             .ForMember(p => p.Exams, opt => opt.MapFrom(dto => new List<Exam>
             {
              new Exam { CompetitionId= dto.ComputaionId, PartsNumber=dto.PartsNumber, EntryId=dto.EntryId,TeacherId=dto.TeacherId, TotalScore=dto.Score}
             }
             ))
             .ForMember(p => p.Education, c => c.MapFrom(s => s.Education));


            CreateMap<CreateContestantDto, PrintContestantDto>()
                .ReverseMap();


            CreateMap<Contestant, ContestantDetailsDto>()
                .ForMember(p => p.Exams, c => c.MapFrom(c => c.Exams));



            CreateMap<Contestant, GetallContestantsDto>()
                .ForMember(p => p.Score, c => c.MapFrom(s => s.Exams.LastOrDefault().TotalScore))
                .ForMember(p => p.Computaions, c => c.MapFrom(s => s.Exams.Select(p => p.Competition).DistinctBy(p => p.Date)))
             .ForMember(p => p.NumberOfParts, opt => opt.MapFrom(dto => dto.Exams.LastOrDefault().PartsNumber));


        }
    }
}
