using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class SurveyEntity : IIdentity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public UserEntity User { get; set; }
        public List<SurveyQuestionEntity> SurveyQuestions { get; set; } = new List<SurveyQuestionEntity>();
    }
}
