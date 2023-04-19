using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISurveyUserService
    {
        IEnumerable<Survey> GetAllSurveys();
        Survey? GetSurveyById(int surveyId);
        SurveyQuestionUserAnswer SaveUserAnswerForSurvey(int surveyId, int surveyQuestionId, string answer);
        List<SurveyQuestionUserAnswer> GetUserAnswersForSurvey (int surveyId, int userId);
    }
}
