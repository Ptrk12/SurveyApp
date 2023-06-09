﻿using ApplicationCore.Models;
using Infrastructure.Entities;
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
    public class UserRepository :IUserRepository
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

        public string GetDomainFromEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Host;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool IsEmailFromDomain(string email, string domain)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Host.Equals(domain, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public string RemoveDomainFromEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.User;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool CheckIfUserAdmin()
        {
            var email = GetUserEmailFromTokenJwt();

            var query = (from u in _context.Users
                         join r in _context.UserRoles
                         on u.Id equals r.UserId
                         where r.RoleId == 2
                         && u.Email == email
                         group u by u.Id into grouped
                         select grouped.Count());

            return  query.Count() > 0 ? true : false;
        }

        public bool CheckIfUserAnswer(int surveyAnswerId)
        {
            var userId = GetUserIdFromTokenJwt();

            var query = (from ua in _context.UserAnswers
                        where ua.UserId == int.Parse(userId)
                        && ua.Id== surveyAnswerId
                        group ua by ua.Id into grouped
                        select grouped.Count());

            return query.Count() > 0? true: false;
        }
    }
    public interface IUserRepository
    {
        bool CheckIfItUserSurvey(int surveyId);
        string? GetUserEmailFromTokenJwt();
        public string GetUserIdFromTokenJwt();
        bool IsEmailFromDomain(string email, string domain);
        string RemoveDomainFromEmail(string email);
        string GetDomainFromEmail(string email);
        bool CheckIfUserAdmin();
        bool CheckIfUserAnswer(int surveyAnswerId);
    }
}
