﻿using ApplicationCore.Const;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Infrastructure.Repositories;

namespace Managers.Managers
{
    public class UserAnswerManager : IUserAnswerManager
    {
        private ISurveyQuestionRepository _surveyQuestionRepository;
        private ISurveyRepository _surveyRepository;
        private IUserRepository _userRepository;
        private IUserAnswerRepository _userAnswerRepository;

        public UserAnswerManager(
            ISurveyQuestionRepository surveyQuestionRepository, 
            ISurveyRepository surveyRepository, 
            IUserRepository userRepository,
            IUserAnswerRepository userAnswerRepository)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyRepository = surveyRepository;
            _userRepository = userRepository;
            _userAnswerRepository = userAnswerRepository;
        }


        public async Task<bool> SaveAnswerWithQuestionType(SurveyQuestionEntity foundQuestion, UserAnswerDto dto, int surveyId, int questionId)
        {
            var isParsed = int.TryParse(_userRepository.GetUserIdFromTokenJwt(), out int result);
            if (foundQuestion.Type == QuestionTypes.Numeric)
            {
                if (int.Parse(dto.Answer) >= 0 && int.Parse(dto.Answer) <= 10)
                {
                    var answer = _userAnswerRepository.SaveUserAnswer(dto, surveyId, questionId, isParsed == true ? result : null);
                    await _surveyQuestionRepository.Save();
                    foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    await _surveyQuestionRepository.Save();
                    return true;
                }
            }
            else if (foundQuestion.Type == QuestionTypes.Multiple)
            {
                if (dto.Answer.Split("_").Length <= foundQuestion.NumberOfMaxAnswers && foundQuestion.NumberOfMaxAnswers != null)
                {
                    var answer = _userAnswerRepository.SaveUserAnswer(dto, surveyId, questionId, isParsed == true ? result : null);
                    await _surveyQuestionRepository.Save();
                    foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                    await _surveyQuestionRepository.Save();
                    return true;
                }
            }
            else
            {
                var answer = _userAnswerRepository.SaveUserAnswer(dto, surveyId, questionId, isParsed == true ? result : null);
                await _surveyQuestionRepository.Save();
                foundQuestion.SurveyQuestionAnswers.Add(SurveyMapper.FromSurveyQuestionAnswerToQuestionAnswer(answer));
                await _surveyQuestionRepository.Save();
                return true;
            }
            return false;
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

                if (userEmail != null)
                {
                    allowedDomain = _userRepository.GetDomainFromEmail(foundSurvey.UserEmail);
                }

                if (foundSurvey != null)
                {
                    var isDomainEmail = _userRepository.IsEmailFromDomain(userEmail, allowedDomain);
                    if (foundSurvey.Status == SurveyTypes.Private && userEmail == null && userId == null)
                    {
                        return 0;
                    }
                    if (foundSurvey.Status == SurveyTypes.Domain && (userEmail == null || isDomainEmail == false && userId == null))
                    {
                        return 2;
                    }
                }

                if (foundQuestion != null)
                {
                    bool isSave = false;

                    if (_userRepository.CheckIfUserAdmin() == true)
                    {
                        isSave = await SaveAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId);
                    }
                    if (foundSurvey.Status == SurveyTypes.Private && userEmail != null && userId != null)
                    {
                        isSave = await SaveAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId);
                    }
                    else if (foundSurvey.Status == SurveyTypes.Domain && userEmail != null && userId != null)
                    {
                        if (_userRepository.IsEmailFromDomain(userEmail, allowedDomain))
                        {
                            isSave = await SaveAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId);
                        }
                    }
                    else if (foundSurvey.Status == SurveyTypes.Public)
                    {
                        isSave = await SaveAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId);
                    }
                    if (isSave == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
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

        public async Task<int> EditUserAnswer(UserAnswerDto dto, int answerId, int surveyId, int questionId)
        {
            try
            {
                var userEmail = _userRepository.GetUserEmailFromTokenJwt();
                var userId = _userRepository.GetUserIdFromTokenJwt();

                var foundSurvey = await _surveyRepository.GetByIdAsync(surveyId);

                var foundQuestion = await _surveyQuestionRepository.GetByIdAsync(questionId);

                var allowedDomain = String.Empty;

                if (userEmail != null)
                {
                    allowedDomain = _userRepository.GetDomainFromEmail(foundSurvey.UserEmail);
                }

                if (foundSurvey != null)
                {
                    var isDomainEmail = _userRepository.IsEmailFromDomain(userEmail, allowedDomain);

                    if (foundSurvey.Status == SurveyTypes.Private && userEmail == null && userId == null)
                    {
                        return 0;
                    }
                    if (foundSurvey.Status == SurveyTypes.Domain && (userEmail == null || isDomainEmail == false && userId == null))
                    {
                        return 2;
                    }
                }

                if (foundQuestion != null)
                {
                    bool isSave = false;
                    if (_userRepository.CheckIfUserAdmin() == true)
                    {
                        isSave = EditAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId, answerId);
                    }
                    if (foundSurvey.Status == SurveyTypes.Private && userEmail != null && userId != null)
                    {
                        isSave = EditAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId, answerId);
                    }
                    else if (foundSurvey.Status == SurveyTypes.Domain && userEmail != null && userId != null)
                    {
                        if (_userRepository.IsEmailFromDomain(userEmail, allowedDomain))
                        {
                            isSave = EditAnswerWithQuestionType(foundQuestion, dto, surveyId, questionId, answerId);
                        }
                    }
                    else if (foundSurvey.Status == SurveyTypes.Public)
                    {
                        isSave = false;
                    }
                    if (isSave == true)
                    {
                        await _surveyQuestionRepository.Save();
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
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
        public bool EditAnswerWithQuestionType(SurveyQuestionEntity foundQuestion, UserAnswerDto dto, int surveyId, int questionId, int answerId)
        {
            var isParsed = int.TryParse(_userRepository.GetUserIdFromTokenJwt(), out int result);
            if (foundQuestion.Type == QuestionTypes.Numeric)
            {
                if (int.Parse(dto.Answer) >= 0 && int.Parse(dto.Answer) <= 10)
                {
                    _userAnswerRepository.EditUserAnswer(dto, answerId);
                    return true;
                }
            }
            else if (foundQuestion.Type == QuestionTypes.Multiple)
            {
                if (dto.Answer.Split("_").Length <= foundQuestion.NumberOfMaxAnswers && foundQuestion.NumberOfMaxAnswers != null)
                {
                    _userAnswerRepository.EditUserAnswer(dto, answerId);
                    return true;
                }
            }
            else
            {
                _userAnswerRepository.EditUserAnswer(dto, answerId);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAnswer(int userAnswerId)
        {
            try
            {
                if (_userRepository.CheckIfUserAnswer(userAnswerId))
                {
                    _userAnswerRepository.DeleteUserAnswer(userAnswerId);
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

    public interface IUserAnswerManager
    {
        Task<bool> DeleteUserAnswer(int userAnswerId);
        Task<int> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId);
        Task<int> EditUserAnswer(UserAnswerDto dto, int surveyId, int questionId, int userAnswerId);
        Task<bool> SaveAnswerWithQuestionType(SurveyQuestionEntity foundQuestion, UserAnswerDto dto, int surveyId, int questionId);
        bool EditAnswerWithQuestionType(SurveyQuestionEntity foundQuestion, UserAnswerDto dto, int surveyId, int questionId, int answerId);
    }
}
