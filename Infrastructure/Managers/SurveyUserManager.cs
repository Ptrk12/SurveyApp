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
    public class SurveyUserManager:ISurveyUserManager
    {
        private ISurveyRepository _surveyRepository;

        public SurveyUserManager(ISurveyRepository surveyRepository)
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
                await _surveyRepository.RemoveById(id);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }

    public interface ISurveyUserManager
    {
        Task<List<Survey>> GetAll();
        Task<bool> RemoweSurveyById(int id);
    }
}
