using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Context;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess_Layer.Interfaces
{
    public interface IUnitOfWork:IDisposable 
    {
        public MyContext _Context { get; }

        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IBaseRepo<Contestant> ContestantRepo { get; }
        IBaseRepo<Exam> ExamRepo { get; }
        IBaseRepo<Teacher> TeacherRepo { get; }
        IBaseRepo<Competition> CompetitionRepo { get; }
        IBaseRepo<Question> QuestionRepo { get; }
        IBaseRepo<PartRegistration> PartsRegistrationRepo { get; }

        ValueTask Complete();

    }
}
