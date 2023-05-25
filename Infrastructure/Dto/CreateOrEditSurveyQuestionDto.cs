using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class CreateOrEditSurveyQuestionDto
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Question { get; set; }
    }
}
