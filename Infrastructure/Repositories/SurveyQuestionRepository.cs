using ApplicationCore.Commons;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Question = dto.Question
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

        public void EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int id)
        {
            var surveyQuestionEntity = _context.SurveyQuestions.Where(x => x.Id == id).FirstOrDefault();

            if(surveyQuestionEntity != null)
            {
                surveyQuestionEntity.Type = dto.Type;
                surveyQuestionEntity.Question = dto.Question;
            }
        }

        public SurveyQuestionUserAnswerEntity? SaveUserAnswer(UserAnswerDto dto, int surveyId, int surveyQuestionId, int? userId)
        {
            var answer = new SurveyQuestionUserAnswerEntity()
            {
                SurveyId = surveyId,
                SurveyQuestionId = surveyQuestionId,
                UserId = userId,
                Answer = dto.Answer
            };
            try
            {
                _context.UserAnswers.Add(answer);
                return answer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestionEntity, int>
    {
        SurveyQuestionEntity? CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto);
        SurveyQuestionUserAnswerEntity? SaveUserAnswer(UserAnswerDto dto, int surveyId, int surveyQuestionId, int? userId);
        void EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int id);
    }
}
