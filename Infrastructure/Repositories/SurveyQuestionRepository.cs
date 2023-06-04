using ApplicationCore.Commons;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SurveyQuestionRepository : GenericRepository<SurveyQuestionEntity,int>, ISurveyQuestionRepository
    {
        private readonly SurveyDbContext _context;
        public SurveyQuestionRepository(SurveyDbContext context): base(context) 
        {
            _context = context;
        }

        public SurveyQuestionEntity? CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto)
        {
            var questionToAdd = new SurveyQuestionEntity()
            {
                Type = dto.Type,
                Question = dto.Question,
                NumberOfMaxAnswers = dto.NumberOfMaxAnswers
            };

            try
            {
                _context.SurveyQuestions.Add(questionToAdd);
                return questionToAdd;
            }catch (Exception) 
            {
                return null;
            }
        }

        public void DeleteSurveyQuestion(int surveyQuestionId)
        {
            var question = _context.SurveyQuestions.Where(x => x.Id == surveyQuestionId).FirstOrDefault();

            if (question != null)
            {
                _context.SurveyQuestions.Remove(question);
            }
        }

        public void EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int id)
        {
            var surveyQuestionEntity = _context.SurveyQuestions.Where(x => x.Id == id).FirstOrDefault();

            if (surveyQuestionEntity != null)
            {
                surveyQuestionEntity.Type = dto.Type;
                surveyQuestionEntity.Question = dto.Question;
                surveyQuestionEntity.NumberOfMaxAnswers = dto.NumberOfMaxAnswers;
            }
        }
    }

    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestionEntity, int>
    {
        SurveyQuestionEntity? CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto);
        void EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int id);
        void DeleteSurveyQuestion(int surveyQuestionId);
    }
}
