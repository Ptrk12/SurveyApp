
using ApplicationCore.Commons;
using ApplicationCore.Dto;
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
            var foundSurvey = _context.Surveys.Where(x => x.Id == surveyId)
                .Include(x=>x.SurveyQuestions)
                .ThenInclude(x=>x.SurveyQuestionAnswers)
                .FirstOrDefault();

            if(foundSurvey != null)
            {
                var answers = foundSurvey.SurveyQuestions.Select(x => x.SurveyQuestionAnswers.Count).FirstOrDefault();
                return answers != 0 ? true : false;
            }
            return false;
        }

        public void UpdateSurvey(CreateOrEditSurveyDto dto, int id)
        {
            var foundSurvey = _context.Surveys.Where(x => x.Id == id).FirstOrDefault();
            if(foundSurvey != null)
            {
                foundSurvey.Title = dto.Title;
                foundSurvey.Status = dto.Status;
            }
        }
    }

    public interface ISurveyRepository : IGenericRepository<SurveyEntity, int> 
    {
        Task<List<SurveyEntity>> GetSurveysInclude();
        bool CheckIfSurveyHasAnswers(int surveyId);
        void UpdateSurvey(CreateOrEditSurveyDto dto, int id);
    }
}
