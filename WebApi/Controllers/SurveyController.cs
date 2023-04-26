using ApplicationCore.Models;
using Infrastructure.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private ISurveyUserManager _userManager;

        public SurveyController(ISurveyUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _userManager.GetAll();
            return Ok(entities);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = _userManager.RemoweSurveyById(id);
            return deleted.Result == true ? Ok(deleted) : NotFound();
        }
    }
}
