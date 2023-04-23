
using ApplicationCore.Commons;
using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SurveyRepository:GenericRepository<SurveyEntity, int>, ISurveyRepository
    {
        public SurveyRepository(SurveyDbContext _context): base(_context) { }

    }

    public interface ISurveyRepository : IGenericRepository<SurveyEntity, int> { }
}
