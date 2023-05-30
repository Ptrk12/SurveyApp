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
        public async Task<IActionResult> CreateNewQuestionForSpecificSurvey(CreateOrEditSurveyQuestionDto dto,int surveyId)
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
        [HttpPut]
        [Route("surveyId/{questionId}")]
        [Authorize]
        public async Task<IActionResult> EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int surveyId, int questionId)
        {
            var result = await _surveyQuestionManager.EditSurveyQuestion(dto, surveyId, questionId);

            return result == true ? Ok(): BadRequest();
        }

        [HttpPut]
        [Route("{surveyId}/{questionId}/{answerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditSurveyQuestion(UserAnswerDto dto, int surveyId, int questionId, int answerId)
        {
            var result = await _surveyQuestionManager.EditUserAnswer(dto,answerId, surveyId, questionId);

            if (result == 2)
                return Forbid();
            if (result == -1)
                return BadRequest();
            if (result == 0)
                return Unauthorized();
            return Ok();
        }

        [HttpDelete]
        [Route("{surveyId}/{questionId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSurveyQuestion(int surveyId, int questionId)
        {
            var result = await _surveyQuestionManager.DeleteSurveyQuestion(questionId, surveyId);

            return result == true? Ok():BadRequest();
        }
    }
}
