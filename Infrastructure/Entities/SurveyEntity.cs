﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class SurveyEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<SurveyQuestionEntity> SurveyQuestions { get; set; }
    }
}