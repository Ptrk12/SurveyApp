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
    [Route("api/survey")]
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
            var deleted = await _surveyManager.RemoweSurveyById(surveyId);
            return deleted == true ? Ok(deleted) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSurvey(CreateOrEditSurveyDto dto)
        {
            var result = await _surveyManager.CreateNewSurvey(dto);
            return result == true? Ok() : BadRequest();   
        }
        [HttpPut]
        [Route("{surveyId}")]
        public async Task<IActionResult> EditSurvey(CreateOrEditSurveyDto dto, int surveyId)
        {
            var result = _surveyManager.EditSurvey(dto, surveyId);
            return result.Result==true ? Ok() : BadRequest();
        }
    }
}
