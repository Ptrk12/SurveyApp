
using ApplicationCore.Dto;
using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Managers
{
    public class SurveyManager:ISurveyManager
    {
        private ISurveyRepository _surveyRepository;
        private IUserRepository _userRepository;

        public SurveyManager(
            ISurveyRepository surveyRepository,
            IUserRepository userRepository)
        {
            _surveyRepository = surveyRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateNewSurvey(CreateSurveyDto survey)
        {
            try
            {
                var entityToAdd = new SurveyEntity()
                {
                    Title = survey.Title,
                    Status = survey.Status,
                    UserId = int.Parse(_userRepository.GetUserIdFromTokenJwt()),
                    UserEmail = _userRepository.GetUserEmailFromTokenJwt(),
                };
                await _surveyRepository.Add(entityToAdd);
                await _surveyRepository.Save();
                return true;
            }catch (Exception) 
            {
                return false;
            }
        }

        public async Task<List<Survey>> GetAll()
        {
            var items = await _surveyRepository.GetSurveysInclude();
            var result = items.Select(x => SurveyMapper.FromEntityToSurvey(x)).ToList();
            return result;
        }

        public async Task<bool> RemoweSurveyById(int id)
        {
            try
            {
                var chckIfAnswers = _surveyRepository.CheckIfSurveyHasAnswers(id);
                var chckIfUserSurvey = _userRepository.CheckIfItUserSurvey(id);

                if ((chckIfAnswers == false && chckIfUserSurvey == true) || _userRepository.CheckIfUserAdmin() == true)
                {
                    await _surveyRepository.RemoveById(id);
                    await _surveyRepository.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> EditSurvey(CreateSurveyDto dto, int id)
        {
            try
            {
                var itIsUserSurvey = _userRepository.CheckIfItUserSurvey(id);
                var surveyHasAnswers = _surveyRepository.CheckIfSurveyHasAnswers(id);
                var isAdmin = _userRepository.CheckIfUserAdmin();
                if ((itIsUserSurvey == true && surveyHasAnswers == false) || isAdmin == true)
                {
                    _surveyRepository.UpdateSurvey(dto, id);
                    await _surveyRepository.Save();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
    }

    public interface ISurveyManager
    {
        Task<List<Survey>> GetAll();
        Task<bool> RemoweSurveyById(int id);
        Task<bool> CreateNewSurvey(CreateSurveyDto survey);
        Task<bool> EditSurvey(CreateSurveyDto dto, int id);
    }
}
