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

        public SurveyQuestionManager(
            ISurveyQuestionRepository surveyQuestionRepository
            , ISurveyRepository surveyRepository)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyRepository = surveyRepository;
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
    }

    public interface ISurveyQuestionManager
    {
        Task<SurveyQuestion?> CreateNewSurveyQuestion(CreateSurveyQuestionDto dto, int surveyId);
    }
}
