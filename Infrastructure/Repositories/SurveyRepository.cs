
using ApplicationCore.Commons;
using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SurveyRepository:GenericRepository<SurveyEntity, int>, ISurveyRepository
    {
        private readonly SurveyDbContext _context;
        public SurveyRepository(SurveyDbContext context): base(context) 
        {
            _context = context;
        }

        //TO be refactored
        public async Task<List<SurveyEntity>> GetSurveysInclude()
        {
            var result = await _context.Surveys
                .Include(x => x.SurveyQuestions)
                .ThenInclude(x => x.SurveyQuestionAnswers)
                .ToListAsync();         
            
            return result;
        }

        public bool CheckIfSurveyHasAnswers(int surveyId)
        {
            var result = (from s in _context.Surveys
                          join u in _context.SurveyAnswers
                          on s.Id equals u.Id
                          where s.Id == surveyId
                          group u by u.Id into grouped
                          select grouped.Count());

            if (result.Count() != 0)
            {
                return true;
            }
            return false;
        }
    }

    public interface ISurveyRepository : IGenericRepository<SurveyEntity, int> 
    {
        Task<List<SurveyEntity>> GetSurveysInclude();
        bool CheckIfSurveyHasAnswers(int surveyId);
    }
}
