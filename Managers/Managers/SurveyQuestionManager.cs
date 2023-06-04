using ApplicationCore.Const;
using ApplicationCore.Models;
using Infrastructure.Dto;
using Infrastructure.Entities;
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

        public async Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int surveyId)
        {
            try
            {
                var questionToAdd = _surveyQuestionRepository.CreateNewSurveyQuestion(dto);

                if(dto.Type != QuestionTypes.Numeric && dto.Type != QuestionTypes.Multiple &&
                    dto.Type != QuestionTypes.Open)
                {
                    return null;
                }

                await _surveyQuestionRepository.Save();

                var foundSurvey = await _surveyRepository.GetByIdAsync(surveyId);
                var isUserSurvey = _userRepository.CheckIfItUserSurvey(surveyId);
                var isUserAdmin = _userRepository.CheckIfUserAdmin();
                if ((foundSurvey != null && questionToAdd != null && isUserSurvey||isUserAdmin))
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

        public async Task<bool> DeleteSurveyQuestion(int questionId, int surveyId)
        {
            try
            {
                var isUserSurvey = _userRepository.CheckIfItUserSurvey(surveyId);
                var isUserAdmin = _userRepository.CheckIfUserAdmin();

                if(isUserSurvey == true || isUserAdmin == true)
                {
                     _surveyQuestionRepository.DeleteSurveyQuestion(questionId);
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
        public async Task<bool> EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int questionId, int surveyId)
        {
            try
            {
                if (dto.Type != QuestionTypes.Numeric && dto.Type != QuestionTypes.Multiple &&
                    dto.Type != QuestionTypes.Open)
                {
                    return false;
                }
                var foundSurvey = await _surveyRepository.GetByIdAsync(surveyId);

                if ((foundSurvey != null && dto != null && _userRepository.CheckIfItUserSurvey(surveyId) || _userRepository.CheckIfUserAdmin()))
                {
                    _surveyQuestionRepository.EditSurveyQuestion(dto, questionId);
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
        Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int surveyId);
        Task<bool> EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int questionId, int surveyId);
        Task<bool> DeleteSurveyQuestion(int questionId, int surveyId);    
    }
}
