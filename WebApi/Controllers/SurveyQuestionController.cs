using Infrastructure.Dto;
using Managers.Managers;
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
    }
}
