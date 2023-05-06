using ApplicationCore.Dto;
using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private ISurveyManager _surveyManager;

        public SurveyController(ISurveyManager userManager)
        {
            _surveyManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _surveyManager.GetAll();
            return Ok(entities);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        [Authorize(Policy = "Bearer")]
        public async Task<IActionResult> DeleteById(int surveyId)
        {
            var deleted = _surveyManager.RemoweSurveyById(surveyId);
            return deleted.Result == true ? Ok(deleted) : BadRequest();
        }

        [HttpPost("surveyadd")]
        public async Task<IActionResult> CreateNewSurvey(CreateSurveyDto dto)
        {
            await _surveyManager.CreateNewSurvey(dto);
            return Ok();
        }
    }
}
