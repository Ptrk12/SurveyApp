using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SurveyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(SurveyDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CheckIfItUserSurvey(int surveyId)
        {
            var userId = GetUserIdFromTokenJwt();

            var result = (from s in _context.Surveys
                          join u in _context.Users
                          on s.UserId equals u.Id
                          where s.Id == surveyId
                          && s.UserId == int.Parse(userId)
                          group u by u.Id into grouped
                          select grouped.Count());


            return result.Count() != 0 ? true : false;
        }

        public string GetUserIdFromTokenJwt()
        {
            var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return result;
        }

        public string? GetUserEmailFromTokenJwt()
        {
            var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            return result;
        }
    }
    public interface IUserRepository
    {
        bool CheckIfItUserSurvey(int surveyId);
        string? GetUserEmailFromTokenJwt();
        public string GetUserIdFromTokenJwt();
    }
}
