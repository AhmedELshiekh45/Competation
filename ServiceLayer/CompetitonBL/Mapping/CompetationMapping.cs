using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_Layer.Models;
using ServiceLayer.CompetitonBL.Dtos;

namespace ServiceLayer.CompetitonBL.Mapping
{
    public class CompetationMapping : Profile
    {
        public CompetationMapping()
        {
            CreateMap<CreateCompetionDto, Competition>()
            .ForMember(p => p.Id, s => s.Ignore());

            CreateMap<Competition, CreateCompetionDto>();

            CreateMap<Competition, CompetaionDto>()
             .ForMember(dest => dest.ContestantsNumber, opt => opt.MapFrom(src => src.Exams.Count()));

        }
    }
}
