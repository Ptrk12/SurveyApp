﻿using ApplicationCore.Const;
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

                if ((foundSurvey != null && questionToAdd != null && _userRepository.CheckIfItUserSurvey(surveyId)||_userRepository.CheckIfUserAdmin()))
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
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<int> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId)
        {
            try
            {
                var userEmail = _userRepository.GetUserEmailFromTokenJwt();
                var userId = _userRepository.GetUserIdFromTokenJwt();

                var foundSurvey = await _surveyRepository.GetByIdAsync(surveyId);        

                var foundQuestion = await _surveyQuestionRepository.GetByIdAsync(questionId);

                var allowedDomain = String.Empty;

                if(userEmail != null)
                {
                    allowedDomain = _userRepository.GetDomainFromEmail(foundSurvey.UserEmail);
                }

                if(foundSurvey != null)
                {
                    if(foundSurvey.Status == SurveyTypes.Private && userEmail == null && userId == null)
                    {
                        return 0;
                    }
                    if(foundSurvey.Status == SurveyTypes.Domain && (userEmail != null || !_userRepository.IsEmailFromDomain(userEmail, allowedDomain) && userId != null)) 
                    {
                        return 2;
                    }
                }

                if(foundQuestion != null) 
                {
                    if (_userRepository.CheckIfUserAdmin() == true)
                    {
                        var answer = _surveyQuestionRepository.SaveUserAnswer(dto, surveyId, questionId, (int.Parse(_userRepository.GetUserIdFromTokenJwt())));
                        foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    }
                    if (foundSurvey.Status == SurveyTypes.Private && userEmail != null && userId != null)
                    {
                        var answer = _surveyQuestionRepository.SaveUserAnswer(dto, surveyId, questionId, (int.Parse(_userRepository.GetUserIdFromTokenJwt())));
                        foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    }
                    else if (foundSurvey.Status == SurveyTypes.Domain && userEmail != null && userId != null)
                    {
                        if (_userRepository.IsEmailFromDomain(userEmail, allowedDomain))
                        {
                            var answer = _surveyQuestionRepository.SaveUserAnswer(dto, surveyId, questionId, null);
                            foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                        }
                    }
                    else
                    {
                        var answer = _surveyQuestionRepository.SaveUserAnswer(dto, surveyId, questionId, null);
                        foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    }

                    await _surveyQuestionRepository.Save();
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }

    public interface ISurveyQuestionManager
    {
        Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int surveyId);
        Task<int> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId);
        Task<bool> EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int questionId, int surveyId);
    }
}
