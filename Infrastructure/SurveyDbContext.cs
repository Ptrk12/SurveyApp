﻿using ApplicationCore.Models;
using Infrastructure.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SurveyDbContext:IdentityDbContext<UserEntity,UserRoleEntity,int>
    {
        public DbSet<SurveyEntity> Surveys { get; set; }
        public DbSet<SurveyQuestionEntity> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionUserAnswerEntity> UserAnswers { get; set; }
        public DbSet<SurveyQuestionAnswerEntity> SurveyAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-QR36UPA;Initial Catalog=SurveyApp;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<UserRoleEntity>();

            modelBuilder.Entity<UserRoleEntity>().HasData(new UserRoleEntity
            { Id = 1, Name = "normaluser", NormalizedName = "NORMALUSER".ToUpper() });

            modelBuilder.Entity<UserRoleEntity>().HasData(new UserRoleEntity
            { Id = 2, Name = "administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    UserName = "normaluser",
                    NormalizedUserName = "NORMALUSER",
                    Email = "myuser@email.com",
                    NormalizedEmail = "MYUSER@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Test123!")
                },
                new UserEntity
                {
                    Id = 2,
                    UserName = "administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    Email = "admin@email.com",
                    NormalizedEmail = "ADMIN@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Test123!")
                }
            );



            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                },
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 2
                }
            );

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Surveys)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<SurveyQuestionUserAnswerEntity>()
                .HasOne(x => x.SurveyQuestion);

            modelBuilder.Entity<SurveyQuestionAnswerEntity>()
                .HasData(
                new SurveyQuestionAnswerEntity() { Id = 1, Answer = "4" },
                new SurveyQuestionAnswerEntity() { Id = 2, Answer = "7" },
                new SurveyQuestionAnswerEntity() { Id = 3, Answer = "9" },
                new SurveyQuestionAnswerEntity() { Id = 4, Answer = "Park Forest" },
                new SurveyQuestionAnswerEntity() { Id = 5, Answer = "Cycling Running" },
                new SurveyQuestionAnswerEntity() { Id = 6, Answer = "Pizza Burger" },
                new SurveyQuestionAnswerEntity() { Id = 7, Answer = "Patryk" },
                new SurveyQuestionAnswerEntity() { Id = 8, Answer = "300km" },
                new SurveyQuestionAnswerEntity() { Id = 9, Answer = "12" },
                new SurveyQuestionAnswerEntity() { Id = 10, Answer = "444" });
                

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
                    Title = "My first survey",
                    UserEmail = "myuser@email.com",
                    UserId = 1,
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
                    new { SurveyQuestionAnswersId = 9, SurveyQuestionsId = 9 },
                    new { SurveyQuestionAnswersId = 10, SurveyQuestionsId = 1 }
                    ));
        }
    }
}
