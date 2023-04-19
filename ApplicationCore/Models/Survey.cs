using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
