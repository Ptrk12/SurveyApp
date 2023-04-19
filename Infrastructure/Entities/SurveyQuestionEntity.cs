using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class SurveyQuestionEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public List<SurveyQuestionAnswerEntity> SurveyQuestionAnswers { get; set; }
        public List<SurveyEntity> Surveys { get; set; }
    }
}
