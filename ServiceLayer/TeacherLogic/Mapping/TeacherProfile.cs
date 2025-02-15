using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_Layer.DTOS;
using DataAccess_Layer.Models;
using ServiceLayer.TeacherLogic.Dtos;

namespace ServiceLayer.TeacherLogic.Mapping
{
    public class TeacherProfile :Profile
    {
        public TeacherProfile()
        {
           


            CreateMap<User, RegisterDTO>()
                  .ReverseMap();

            CreateMap<CreateTeacherDto, Teacher>()
            .ForMember(p => p.Id, s => s.Ignore());

            CreateMap<Teacher, CreateTeacherDto>();


            CreateMap<Teacher, TeacherDto>()
                .ForMember(p=>p.NumberOfContestant, s => s.MapFrom(p=>p.ContestantsExams.Count));


            
        }
    }
}
