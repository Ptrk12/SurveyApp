using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class CreateSurveyDto
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
