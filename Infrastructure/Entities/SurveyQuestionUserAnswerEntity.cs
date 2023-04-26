using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class SurveyQuestionUserAnswerEntity : IIdentity<int>
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int SurveyQuestionId { get; set; }
        public SurveyQuestionEntity SurveyQuestion { get; set; }
        public int UserId { get; set; }
        public string Answer { get; set; }
    }
}
