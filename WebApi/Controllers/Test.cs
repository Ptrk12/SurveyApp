using ApplicationCore.Models;
using Infrastructure.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class Test : ControllerBase
    {
        private ISurveyUserManager _userManager;

        public Test(ISurveyUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _userManager.SurveyRepository.GetAllAsync(x=>x.SurveyQuestions);

            //var items = entities.Select(x => new Survey()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Status = x.Status
            //});

            return Ok(entities);
        }
    }
}
