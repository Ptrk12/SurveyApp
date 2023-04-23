using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class SurveyQuestion:IIdentity<int>
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
    }
}
