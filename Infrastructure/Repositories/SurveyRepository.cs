
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

        public async Task<List<SurveyEntity>> GetSurveysInclude()
        {
            var result = _context.Surveys
                .Include(x => x.SurveyQuestions)
                .ThenInclude(x => x.SurveyQuestionAnswers)
                .ToList();         
            
            return result;
        }

        public bool CheckIfSurveyHasAnswers(int surveyId)
        {
            var result = _context.SurveyAnswers
                .Include(x => x.SurveyQuestions)
                .ThenInclude(x => x.Surveys)
                .Where(x => x.Id == surveyId)
                .Count();

            if(result != 0)
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
