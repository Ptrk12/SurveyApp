
using ApplicationCore.Models;
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

        public SurveyManager(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
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
                if(_surveyRepository.CheckIfSurveyHasAnswers(id))
                {
                    await _surveyRepository.RemoveById(id);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface ISurveyManager
    {
        Task<List<Survey>> GetAll();
        Task<bool> RemoweSurveyById(int id);
    }
}
