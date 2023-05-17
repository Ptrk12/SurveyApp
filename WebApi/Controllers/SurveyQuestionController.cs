using Infrastructure.Dto;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    [Route("api/surveyQuestion")]
    [ApiController]
    public class SurveyQuestionController : ControllerBase
    {
        private ISurveyQuestionManager _surveyQuestionManager;

        public SurveyQuestionController(ISurveyQuestionManager surveyQuestionManager)
        {
            _surveyQuestionManager = surveyQuestionManager;
        }

        [HttpPost]
        [Route("{surveyId}")]
        public async Task<IActionResult> CreateNewQuestionForSpecificSurvey(CreateSurveyQuestionDto dto,int surveyId)
        {
            var result = await _surveyQuestionManager.CreateNewSurveyQuestion(dto, surveyId);

            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("{surveyId}/{questionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId)
        {
            var result = await _surveyQuestionManager.SaveUserAnswer(dto, surveyId, questionId);

            if (result == 2)
                return Forbid();
            if (result == -1)
                return BadRequest();
            if (result == 0)
                return Unauthorized();
            return Ok();
        }
    }
}
