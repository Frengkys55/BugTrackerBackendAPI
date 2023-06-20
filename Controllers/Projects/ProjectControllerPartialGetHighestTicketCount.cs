using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Controllers.Projects
{
    public partial class ProjectController : ControllerBase
    {
        /// <summary>
        /// Delete a project from database
        /// </summary>
        /// <param name="accesstoken">Your access token</param>
        /// <returns></returns>
        [EnableCors("AllowAllOrigins")]
        [HttpGet("GetHighestTicketCount")]
        public async Task<IActionResult> GetHighestCountingTicket([FromHeader] string accesstoken)
        {
            try
            {
                var result = await new Project().GetHighestTicketCount(accesstoken, _configuration.GetConnectionString("Default"));
                return Ok(result);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return NoContent();
        }
    }
}
