﻿using ApplicationCore.Commons;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SurveyQuestionRepository : GenericRepository<SurveyQuestionEntity,int>, ISurveyQuestionRepository
    {
        private readonly SurveyDbContext _context;
        public SurveyQuestionRepository(SurveyDbContext context): base(context) 
        {
            _context = context;
        }

        public SurveyQuestionEntity? CreateNewSurveyQuestion(CreateSurveyQuestionDto dto)
        {
            var questionToAdd = new SurveyQuestionEntity()
            {
                Type = dto.Type,
                Question = dto.Question
            };

            try
            {
                _context.SurveyQuestions.Add(questionToAdd);
                return questionToAdd;
            }catch (Exception) 
            {
                return null;
            }
        }


    }

    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestionEntity, int>
    {
        SurveyQuestionEntity? CreateNewSurveyQuestion(CreateSurveyQuestionDto dto);
    }
}
