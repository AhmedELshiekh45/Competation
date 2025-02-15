//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using DataAccess_Layer.DTOS;
//using DataAccess_Layer.Models;

//namespace BL_Layer.Mapping
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<User, RegisterDTO>()
//                  .ReverseMap();


//            CreateMap<ComputaionDto, Compuition>()
//            .ForMember(p => p.Id, s => s.Ignore());

//            CreateMap<Compuition, ComputaionDto>();

//            CreateMap<TeacherDto, Teacher>()
//            .ForMember(p => p.Id, s => s.Ignore());

//            CreateMap<Teacher, TeacherDto>();

//            CreateMap<Contestant, GetallContestantsDto>()
//                .ForMember(p => p.Score, c => c.MapFrom(s => s.Exams.LastOrDefault().TotalScore))
//                .ForMember(p => p.Teacher, c => c.MapFrom(s => s.Teacher))
//                .ForMember(p=>p.Computaions,c=>c.MapFrom(s=>s.Exams.Select(p=>p.Compuition).Distinct()))
//             .ForMember(p => p.NumberOfParts, opt => opt.MapFrom(dto => dto.Exams.LastOrDefault().PartsNumber))
             
//             ;

//            CreateMap<Contestant, DetailsDto>()
//                .ForMember(p => p.TeacherName, c => c.MapFrom(c => c.Teacher.Name))
//                .ForMember(p => p.NumberOfParts, opt => opt.MapFrom(dto => dto.Exams.LastOrDefault().PartsNumber))
//                .ForMember(p => p.Exams, c => c.MapFrom(c => c.Exams));



//            CreateMap<Question, QuestionDto>()
//                .ReverseMap();


          



//            // مابينج بين Question و QuestionDto
//        }
//    }
//}
