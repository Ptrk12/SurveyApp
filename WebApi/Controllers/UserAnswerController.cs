using Infrastructure.Dto;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    [Route("api/userAnswer")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswerManager _userAnswerManager;

        public UserAnswerController(IUserAnswerManager userAnswerManager)
        {
            _userAnswerManager = userAnswerManager;
        }

        [HttpDelete]
        [Route("/{userAnswerId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAnswer(int userAnswerId)
        {
            var result = await _userAnswerManager.DeleteUserAnswer(userAnswerId);

            return result == true? Ok(result) : BadRequest();
        }


        [HttpPut]
        [Route("{surveyId}/{questionId}/{answerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditSurveyAnswer(UserAnswerDto dto, int surveyId, int questionId, int answerId)
        {
            var result = await _userAnswerManager.EditUserAnswer(dto, answerId, surveyId, questionId);

            if (result == 2)
                return Forbid();
            if (result == -1)
                return BadRequest();
            if (result == 0)
                return Unauthorized();
            return Ok();
        }

        [HttpPost]
        [Route("{surveyId}/{questionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveUserAnswer(UserAnswerDto dto, int surveyId, int questionId)
        {
            var result = await _userAnswerManager.SaveUserAnswer(dto, surveyId, questionId);

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
