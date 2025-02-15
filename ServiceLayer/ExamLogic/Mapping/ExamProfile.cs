using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Models;
using ServiceLayer.ExamLogic.Dtos;

namespace ServiceLayer.ExamLogic.Mapping
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {

            CreateMap<Exam, ExamDto>()
           .ReverseMap();

            CreateMap<Exam, ExamDetailsDto>()
            .ForMember(p => p.TeacherName, s => s.MapFrom(s => s.Teacher.Name))
            .ForMember(p => p.PartsNumber, s => s.MapFrom(s => s.PartsNumber))
            .ForMember(p=>p.ExamDate,s=>s.MapFrom(s=>s.Competition.Date))
           .ReverseMap();
        }
    }
}
