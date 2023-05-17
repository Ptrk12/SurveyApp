using ApplicationCore.Models;
using Infrastructure.Dto;
using Infrastructure.Managers;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class SurveyQuestionManager:ISurveyQuestionManager
    {
        private ISurveyQuestionRepository _surveyQuestionRepository;
        private ISurveyRepository _surveyRepository;
        private IUserRepository _userRepository;

        public SurveyQuestionManager(
            ISurveyQuestionRepository surveyQuestionRepository
            ,ISurveyRepository surveyRepository
            ,IUserRepository userRepository)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyRepository = surveyRepository;
            _userRepository = userRepository;
        }

        public async Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateSurveyQuestionDto dto, int surveyId)
        {
            try
            {
                var questionToAdd = _surveyQuestionRepository.CreateNewSurveyQuestion(dto);
                await _surveyQuestionRepository.Save();

                var foundSurvey = await _surveyRepository.GetByIdAsync(surveyId);

                if (foundSurvey != null && questionToAdd != null)
                {
                    foundSurvey.SurveyQuestions.Add(questionToAdd);
                    await _surveyQuestionRepository.Save();
                    var result = SurveyMapper.FromEntityToSurveyQuestion(questionToAdd);
                    return result;
                }
                return null;
            }
            catch (Exception) 
            {
                return null;
            }
        }

        public async Task<bool> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId)
        {
            try
            {
                var answer = _surveyQuestionRepository.SaveUserAnswer(dto, surveyId, questionId, (int.Parse(_userRepository.GetUserIdFromTokenJwt())));

                var foundQuestion = await _surveyQuestionRepository.GetByIdAsync(questionId);

                if(answer != null && foundQuestion != null) 
                {
                    foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    await _surveyQuestionRepository.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface ISurveyQuestionManager
    {
        Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateSurveyQuestionDto dto, int surveyId);
        Task<bool> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId);
    }
}
