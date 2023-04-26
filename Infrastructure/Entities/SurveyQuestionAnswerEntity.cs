using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class SurveyQuestionAnswerEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public List<SurveyQuestionEntity> SurveyQuestions { get; set;}
    }
}
