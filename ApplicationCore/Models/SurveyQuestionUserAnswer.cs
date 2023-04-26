using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class SurveyQuestionUserAnswer
    {
        public int Id
        {
            get => Convert.ToInt32($"{SurveyId}{SurveyQuestion.Id}{UserId}");
            set { }
        }
        public int SurveyId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public int UserId { get; set; }
        public string Answer { get; set; }
    }
}
