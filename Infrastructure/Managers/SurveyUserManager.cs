using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Managers
{
    public interface ISurveyUserManager
    {
        ISurveyRepository SurveyRepository { get; }
        void Save();
    }
    public class SurveyUserManager : ISurveyUserManager
    {
        private SurveyDbContext _context;
        private ISurveyRepository _surveyRepository;

        public SurveyUserManager(SurveyDbContext context)
        {
            _context = context;
        }

        public ISurveyRepository SurveyRepository
        {
            get
            {
                if(_surveyRepository == null)
                {
                    _surveyRepository = new SurveyRepository(_context);
                }

                return _surveyRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
