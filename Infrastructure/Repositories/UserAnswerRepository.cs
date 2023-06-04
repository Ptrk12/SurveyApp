using Infrastructure.Dto;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserAnswerRepository:IUserAnswerRepository
    {
        private readonly SurveyDbContext _context;

        public UserAnswerRepository(SurveyDbContext context)
        {
            _context = context;
        }

        public SurveyQuestionUserAnswerEntity? SaveUserAnswer(UserAnswerDto dto, int surveyId, int surveyQuestionId, int? userId)
        {
            var answer = new SurveyQuestionUserAnswerEntity()
            {
                SurveyId = surveyId,
                SurveyQuestionId = surveyQuestionId,
                UserId = userId,
                Answer = dto.Answer.Replace('_', ' ')
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
        public void DeleteUserAnswer(int surveyUserAnswerId)
        {
            var answer = _context.UserAnswers.Where(x => x.Id == surveyUserAnswerId).FirstOrDefault();

            var surveyAnswer = _context.SurveyAnswers.Where(x => x.Id == surveyUserAnswerId).FirstOrDefault();

            if (surveyAnswer != null)
            {
                _context.UserAnswers.Remove(answer);
            }
            if (answer != null)
            {
                _context.SurveyAnswers.Remove(surveyAnswer);
            }
        }
        public void EditUserAnswer(UserAnswerDto dto, int surveyUserAnswerId)
        {
            var answer = _context.UserAnswers.Where(x => x.Id == surveyUserAnswerId).First();

            var surveyAnswer = _context.SurveyAnswers.Where(x => x.Id == surveyUserAnswerId).First();

            if (surveyAnswer != null && answer != null)
            {
                surveyAnswer.Answer = dto.Answer;
                answer.Answer = dto.Answer;
            }
        }
    }
    public interface IUserAnswerRepository
    {
        void DeleteUserAnswer(int surveyAnswerId);
        void EditUserAnswer(UserAnswerDto dto, int surveyAnswerId);
        SurveyQuestionUserAnswerEntity? SaveUserAnswer(UserAnswerDto dto, int surveyId, int surveyQuestionId, int? userId);
    }
}
