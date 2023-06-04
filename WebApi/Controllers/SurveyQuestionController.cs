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
        [HttpPut]
        [Route("surveyId/{questionId}")]
        [Authorize]
        public async Task<IActionResult> EditSurveyQuestion(CreateOrEditSurveyQuestionDto dto, int surveyId, int questionId)
        {
            var result = await _surveyQuestionManager.EditSurveyQuestion(dto, questionId, surveyId);

            return result == true ? Ok(): BadRequest();
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
