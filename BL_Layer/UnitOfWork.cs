using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_Layer.Repos;
using DataAccess_Layer.Context;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace BL_Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(UserManager<User> manager, RoleManager<IdentityRole> roleManager, MyContext context)
        {
            UserManager = manager;
            RoleManager = roleManager;
            _Context = context;
            QuestionRepo = new BaseRepo<Question>(context);
            ExamRepo = new BaseRepo<Exam>(context);
            TeacherRepo = new BaseRepo<Teacher>(context);
            ContestantRepo = new BaseRepo<Contestant>(context);
            PartsRegistrationRepo = new BaseRepo<PartRegistration>(context);
            CompetitionRepo = new BaseRepo<Competition>(context);
        }
        public UserManager<User> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }
        public MyContext _Context { get; }

        public IBaseRepo<Contestant> ContestantRepo { get; }

        public IBaseRepo<Exam> ExamRepo { get; }


        public IBaseRepo<Teacher> TeacherRepo { get; }

        public IBaseRepo<Competition> CompetitionRepo { get; }

        public IBaseRepo<Question> QuestionRepo { get; }

        public IBaseRepo<PartRegistration> PartsRegistrationRepo { get; }

        public async ValueTask Complete()
        {
            await _Context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            _Context.Dispose();
        }
    }
}
