﻿using ApplicationCore.Dto;
using ApplicationCore.Models;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class SurveyMapper
    {
        public static List<string> GetAnswersPerSurvey(List<SurveyQuestionAnswerEntity> entities)
        {
            List<string> results= new List<string>();

            foreach (var entity in entities)
            {
                results.Add(entity.Answer);
            }
            return results;
        }

        public static SurveyQuestion FromEntityToSurveyQuestion(SurveyQuestionEntity entity)
        {
            return new SurveyQuestion
            {
                Id = entity.Id,
                Type = entity.Type,
                Question = entity.Question,
                NumberOfMaxAnswers = entity.NumberOfMaxAnswers,
                Answers = GetAnswersPerSurvey(entity.SurveyQuestionAnswers)
            };
        }

        public static Survey FromEntityToSurvey(SurveyEntity entity)
        {
            return new Survey
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status,
                UserEmail= entity.UserEmail,
                SurveyQuestions = entity.SurveyQuestions.Select(x => FromEntityToSurveyQuestion(x)).ToList()
            };
        }

        public static SurveyEntity FromDtoToSurveyEntity(CreateOrEditSurveyDto dto)
        {
            return new SurveyEntity()
            {
                Title = dto.Title,
                Status = dto.Status
            };
        }

        public static SurveyQuestionAnswerEntity FromSurveyQuestionAnswerToQuestionAnswer(SurveyQuestionUserAnswerEntity entity)
        {
            var item = new SurveyQuestionAnswerEntity
            {
                Id = entity.Id,
                Answer = entity.Answer
            };
            return item;
        }
    }
}
