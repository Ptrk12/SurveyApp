using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SurveyDbContext:DbContext
    {
        public DbSet<SurveyEntity> Surveys { get; set; }
        public DbSet<SurveyQuestionEntity> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionUserAnswerEntity> UserAnswers { get; set; }
        public DbSet<SurveyEntity> SurveyAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-BORRVIJ;Initial Catalog=SurveyApp;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SurveyQuestionUserAnswerEntity>()
                .HasOne(x => x.SurveyQuestion);

            modelBuilder.Entity<SurveyQuestionAnswerEntity>()
                .HasData(
                new SurveyQuestionAnswerEntity() { Id = 1, Answer = new string[] { "4" } },
                new SurveyQuestionAnswerEntity() { Id = 2, Answer = new string[] { "7" } },
                new SurveyQuestionAnswerEntity() { Id = 3, Answer = new string[] { "9" } },
                new SurveyQuestionAnswerEntity() { Id = 4, Answer = new string[] { "Park", "Forest" } },
                new SurveyQuestionAnswerEntity() { Id = 5, Answer = new string[] { "Cycling", "Running" } },
                new SurveyQuestionAnswerEntity() { Id = 6, Answer = new string[] { "Pizza", "Burger" } },
                new SurveyQuestionAnswerEntity() { Id = 7, Answer = new string[] { "Patryk" } },
                new SurveyQuestionAnswerEntity() { Id = 8, Answer = new string[] { "300km" } },
                new SurveyQuestionAnswerEntity() { Id = 9, Answer = new string[] { "12" } });

            modelBuilder.Entity<SurveyQuestionAnswerEntity>()
                .Property(e => e.Answer)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<SurveyQuestionEntity>()
                .HasData(
                new SurveyQuestionEntity()
                {
                    Id = 1,
                    Type = "numeric",
                    Question = "How many people live in this house ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 2,
                    Type = "numeric",
                    Question = "How do you rate healthcare in our country ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 3,
                    Type = "numeric",
                    Question = "How do you rate today's weather ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 4,
                    Type = "multiple",
                    Question = "Favourite places to spend free time ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 5,
                    Type = "multiple",
                    Question = "Favourite sports ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 6,
                    Type = "multiple",
                    Question = "Favourite food ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 7,
                    Type = "open",
                    Question = "What's your name ?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 8,
                    Type = "open",
                    Question = "How far is from Cracow to Warsaw?"
                },
                new SurveyQuestionEntity()
                {
                    Id = 9,
                    Type = "open",
                    Question = "4 x 3 ?"
                });

            modelBuilder.Entity<SurveyEntity>()
                .HasData(
                new SurveyEntity()
                {
                    Id = 1,
                    Status = "public",
                    Title = "My first survey"
                });

            modelBuilder.Entity<SurveyEntity>()
                .HasMany<SurveyQuestionEntity>(x => x.SurveyQuestions)
                .WithMany(x => x.Surveys)
                .UsingEntity(
                join => join.HasData(
                    new { SurveysId = 1, SurveyQuestionsId = 1 },
                    new { SurveysId = 1, SurveyQuestionsId = 2 },
                    new { SurveysId = 1, SurveyQuestionsId = 3 },
                    new { SurveysId = 1, SurveyQuestionsId = 4 },
                    new { SurveysId = 1, SurveyQuestionsId = 5 },
                    new { SurveysId = 1, SurveyQuestionsId = 6 },
                    new { SurveysId = 1, SurveyQuestionsId = 7 },
                    new { SurveysId = 1, SurveyQuestionsId = 8 },
                    new { SurveysId = 1, SurveyQuestionsId = 9 }));

            modelBuilder.Entity<SurveyQuestionEntity>()
                .HasMany<SurveyQuestionAnswerEntity>(x => x.SurveyQuestionAnswers)
                .WithMany(x => x.SurveyQuestions)
                .UsingEntity(
                join => join.HasData(
                    new { SurveyQuestionAnswersId = 1, SurveyQuestionsId = 1 },
                    new { SurveyQuestionAnswersId = 2, SurveyQuestionsId = 2 },
                    new { SurveyQuestionAnswersId = 3, SurveyQuestionsId = 3 },
                    new { SurveyQuestionAnswersId = 4, SurveyQuestionsId = 4 },
                    new { SurveyQuestionAnswersId = 5, SurveyQuestionsId = 5 },
                    new { SurveyQuestionAnswersId = 6, SurveyQuestionsId = 6 },
                    new { SurveyQuestionAnswersId = 7, SurveyQuestionsId = 7 },
                    new { SurveyQuestionAnswersId = 8, SurveyQuestionsId = 8 },
                    new { SurveyQuestionAnswersId = 9, SurveyQuestionsId = 9 }
                    ));
        }
    }
}
