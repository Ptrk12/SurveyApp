
using Infrastructure;
using Infrastructure.Managers;
using Infrastructure.Repositories;
using Managers.Managers;
using SurveyApp.Configuration;
using System.Text.Json.Serialization;

namespace SurveyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            ///
            builder.Services.AddDbContext<SurveyDbContext>();
            builder.Services.AddScoped<ISurveyManager, SurveyManager>();
            builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            builder.Services.AddScoped<ISurveyQuestionManager, SurveyQuestionManager>();
            ///

            builder.Services.AddSingleton<JwtSettings>();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
            builder.Services.ConfigureCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.AddUsers();
            app.Run();
        }
    }
}