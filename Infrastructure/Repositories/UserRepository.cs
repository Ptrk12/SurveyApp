using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SurveyDbContext _context;

        public UserRepository(SurveyDbContext context)
        {
            _context = context;
        }

        public bool CheckIfItUserSurvey(int surveyId)
        {
            var result = (from s in _context.Surveys
                          join u in _context.Users
                          on s.UserId equals u.Id
                          where s.Id == surveyId
                          select s).Count();

            return result != 0 ? true : false;
        }

    }
    public interface IUserRepository
    {
        bool CheckIfItUserSurvey(int surveyId);
    }
}
